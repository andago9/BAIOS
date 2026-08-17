using System.Runtime.InteropServices;
using BAIOS.Core;

namespace BAIOS.Maintenance;

public sealed class CleanupTarget
{
    public string Path { get; init; } = "";
    public string Description { get; init; } = "";
    public bool RequiresAdmin { get; init; }
}

public sealed class CleanupPlan
{
    public IReadOnlyList<CleanupTarget> Targets { get; init; } = [];
    public bool EmptyRecycleBin { get; init; } = true;
}

public static class MaintenanceModule
{
    private const string Source = "native:maintenance";

    public static CleanupPlan BuildCleanupPlan()
    {
        var targets = new List<CleanupTarget>
        {
            new()
            {
                Path = System.IO.Path.GetTempPath(),
                Description = "Archivos temporales del usuario (%TEMP%)"
            }
        };

        var windowsTemp = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp");
        targets.Add(new CleanupTarget
        {
            Path = windowsTemp,
            Description = "Temporales de Windows (Windows\\Temp)",
            RequiresAdmin = true
        });

        var localApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var iNetCache = System.IO.Path.Combine(localApp, "Microsoft", "Windows", "INetCache");
        if (Directory.Exists(iNetCache))
        {
            targets.Add(new CleanupTarget
            {
                Path = iNetCache,
                Description = "Caché de Internet de Windows (INetCache)"
            });
        }

        return new CleanupPlan { Targets = targets, EmptyRecycleBin = true };
    }

    public static string ConfirmationText(CleanupPlan plan, bool homeMode)
    {
        var lines = plan.Targets.Select(t => $"• {t.Description}{Environment.NewLine}  {t.Path}").ToList();
        if (plan.EmptyRecycleBin)
        {
            lines.Add("• Papelera de reciclaje");
        }

        var body = string.Join(Environment.NewLine, lines);
        if (homeMode)
        {
            return "Se van a borrar archivos temporales, cachés de Windows y el contenido de la papelera. " +
                   "No se tocan documentos personales. Los archivos en uso se omiten." +
                   Environment.NewLine + Environment.NewLine + body +
                   Environment.NewLine + Environment.NewLine + "¿Continuar?";
        }

        return "Limpiar temporales, cachés y papelera:" + Environment.NewLine + Environment.NewLine + body +
               Environment.NewLine + Environment.NewLine + "¿Continuar?";
    }

    public static Finding RunCleanup(CleanupPlan plan)
    {
        var deleted = 0;
        var skipped = 0;
        var errors = new List<string>();

        foreach (var target in plan.Targets)
        {
            if (!Directory.Exists(target.Path))
            {
                continue;
            }

            if (target.RequiresAdmin && !Elevation.IsAdministrator)
            {
                errors.Add($"{target.Description}: hace falta administrador.");
                continue;
            }

            ClearDirectory(target.Path, ref deleted, ref skipped);
        }

        if (plan.EmptyRecycleBin)
        {
            var hr = SHEmptyRecycleBin(IntPtr.Zero, null, SHERB_NOCONFIRMATION | SHERB_NOPROGRESSUI | SHERB_NOSOUND);
            if (hr != 0 && hr != unchecked((int)0x8000FFFF) && hr != 0x7FFFC)
            {
                errors.Add($"Papelera: código {hr:X8}");
            }
        }

        var detail = $"{deleted} elemento(s) eliminado(s); {skipped} omitido(s) (en uso o sin permiso).";
        if (errors.Count > 0)
        {
            detail += " " + string.Join(" ", errors);
        }

        var severity = errors.Count > 0 && deleted == 0 ? Severity.Fail : errors.Count > 0 ? Severity.Warning : Severity.Ok;
        return Finding.Create(Sections.Maintenance, severity, "Limpieza de temporales y papelera", Source, detail);
    }

    public static Task<CommandResult> RunDismAsync(CancellationToken cancellationToken = default) =>
        CommandRunner.RunAsync(
            System.IO.Path.Combine(Environment.SystemDirectory, "DISM.exe"),
            "/Online /Cleanup-Image /RestoreHealth",
            requireAdmin: true,
            cancellationToken: cancellationToken);

    public static Task<CommandResult> RunSfcAsync(CancellationToken cancellationToken = default) =>
        CommandRunner.RunAsync(
            System.IO.Path.Combine(Environment.SystemDirectory, "sfc.exe"),
            "/scannow",
            requireAdmin: true,
            cancellationToken: cancellationToken);

    public static Task<CommandResult> ScheduleChkdskAsync(string driveLetter, CancellationToken cancellationToken = default)
    {
        var letter = driveLetter.TrimEnd('\\', ':');
        return CommandRunner.RunAsync(
            System.IO.Path.Combine(Environment.SystemDirectory, "cmd.exe"),
            $"/c echo Y| chkdsk {letter}: /F",
            requireAdmin: true,
            cancellationToken: cancellationToken);
    }

    public static Finding FromCommand(string title, CommandResult result)
    {
        if (result.Cancelled)
        {
            return Finding.Create(Sections.Maintenance, Severity.Warning, title, Source, result.Message);
        }

        if (!result.Success)
        {
            return Finding.Create(
                Sections.Maintenance,
                Severity.Fail,
                title,
                Source,
                result.Message ?? $"Código de salida {result.ExitCode}. {Trim(result.CombinedOutput)}");
        }

        return Finding.Create(
            Sections.Maintenance,
            Severity.Ok,
            title,
            Source,
            $"Código de salida {result.ExitCode}. {Trim(result.CombinedOutput)}");
    }

    private static void ClearDirectory(string path, ref int deleted, ref int skipped)
    {
        try
        {
            foreach (var file in Directory.EnumerateFiles(path))
            {
                try
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                    deleted++;
                }
                catch
                {
                    skipped++;
                }
            }

            foreach (var dir in Directory.EnumerateDirectories(path))
            {
                ClearDirectory(dir, ref deleted, ref skipped);
                try
                {
                    Directory.Delete(dir, false);
                    deleted++;
                }
                catch
                {
                    skipped++;
                }
            }
        }
        catch
        {
            skipped++;
        }
    }

    private static string Trim(string text)
    {
        text = text.Trim();
        return text.Length <= 800 ? text : text[..800] + "…";
    }

    private const uint SHERB_NOCONFIRMATION = 0x00000001;
    private const uint SHERB_NOPROGRESSUI = 0x00000002;
    private const uint SHERB_NOSOUND = 0x00000004;

    [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
    private static extern int SHEmptyRecycleBin(IntPtr hwnd, string? pszRootPath, uint dwFlags);
}
