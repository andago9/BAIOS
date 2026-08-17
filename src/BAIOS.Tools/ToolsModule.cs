using System.Diagnostics;
using BAIOS.Core;

namespace BAIOS.Tools;

public static class ToolsModule
{
    private const string SourcePrefix = "tool:";
    public static readonly HashSet<string> CoreIds = new(StringComparer.OrdinalIgnoreCase)
    {
        "adwcleaner", "msert", "autoruns"
    };

    public static ToolCatalog LoadCatalog(string toolsDirectory, string? baseDirectory = null)
    {
        var loaded = ManifestStore.Load(baseDirectory);
        ManifestStore.EnsureRepository(toolsDirectory, loaded.Document);
        var tools = loaded.Document.Tools.Select(ToolCard.FromManifest).ToList();
        return new ToolCatalog
        {
            Tools = tools,
            ManifestPath = loaded.Path,
            FromFile = loaded.FromFile,
            Warnings = loaded.Warnings
        };
    }

    public static IReadOnlyList<ToolCard> VisibleTools(ToolCatalog catalog, bool technician) =>
        technician
            ? catalog.Tools
            : catalog.Tools.Where(t => CoreIds.Contains(t.Id)).ToList();

    public static ToolCard? FindById(ToolCatalog catalog, string id) =>
        catalog.Tools.FirstOrDefault(t => string.Equals(t.Id, id, StringComparison.OrdinalIgnoreCase));

    public static ToolCard Require(ToolCatalog catalog, string id) =>
        FindById(catalog, id)
        ?? ToolCard.FromManifest(ManifestStore.Default().Tools.First(t => t.Id == id));

    public static string? FindTool(string toolsDirectory, ToolCard card)
    {
        var folder = ManifestStore.ToolFolder(toolsDirectory, card.Id);
        var hit = FindInDirectory(folder, card.FileNames, recursive: true);
        if (hit is not null)
        {
            return hit;
        }

        return FindInDirectory(toolsDirectory, card.FileNames, recursive: false);
    }

    public static ToolLaunch Inspect(string toolsDirectory, ToolCard card) =>
        new() { Card = card, Path = FindTool(toolsDirectory, card) };

    public static ToolLaunch Launch(string toolsDirectory, ToolCard card)
    {
        var folder = ManifestStore.ToolFolder(toolsDirectory, card.Id);
        var path = FindTool(toolsDirectory, card);
        if (path is null)
        {
            return Logged(toolsDirectory, new ToolLaunch
            {
                Card = card,
                Message = $"No se encontró el binario. Colócalo en {folder} ({string.Join(", ", card.FileNames)}) o usa Actualizar si hay sha256."
            });
        }

        var blocked = VerifyIntegrity(toolsDirectory, card, path);
        if (blocked is not null)
        {
            return Logged(toolsDirectory, new ToolLaunch
            {
                Card = card,
                Path = path,
                Message = blocked
            });
        }

        var started = DateTimeOffset.Now;
        var arguments = string.Join(" ", card.Arguments.Select(Quote));
        try
        {
            var psi = CommandRunner.LaunchInfo(path, arguments, requireAdmin: card.RequiresAdmin);
            using var process = Process.Start(psi);
            if (process is null)
            {
                return Logged(toolsDirectory, new ToolLaunch
                {
                    Card = card,
                    Path = path,
                    LaunchedAt = started,
                    Message = "No se pudo iniciar el proceso."
                }, arguments);
            }

            process.WaitForExit();
            return Logged(toolsDirectory, new ToolLaunch
            {
                Card = card,
                Path = path,
                LaunchedAt = started,
                ExitCode = process.ExitCode,
                Message = $"Salida {process.ExitCode} a las {started.ToLocalTime():HH:mm:ss}."
            }, arguments, DateTimeOffset.Now - started);
        }
        catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 1223)
        {
            return Logged(toolsDirectory, new ToolLaunch
            {
                Card = card,
                Path = path,
                LaunchedAt = started,
                Message = "UAC cancelado. No se lanzó la herramienta."
            }, arguments, DateTimeOffset.Now - started);
        }
        catch (Exception ex)
        {
            return Logged(toolsDirectory, new ToolLaunch
            {
                Card = card,
                Path = path,
                LaunchedAt = started,
                Message = ex.Message
            }, arguments, DateTimeOffset.Now - started);
        }
    }

    public static void LogSkipped(string toolsDirectory, ToolCard card, string reason) =>
        ExecutionLog.Append(toolsDirectory, card.Id, reason);

    private static ToolLaunch Logged(
        string toolsDirectory,
        ToolLaunch launch,
        string? args = null,
        TimeSpan? duration = null)
    {
        ExecutionLog.Append(
            toolsDirectory,
            launch.Card.Id,
            launch.Message ?? "",
            launch.Path,
            args,
            launch.ExitCode,
            duration);
        return launch;
    }

    public static Finding ToFinding(ToolLaunch launch)
    {
        var source = SourcePrefix + launch.Card.Id;
        if (!launch.Found)
        {
            return Finding.Create(Sections.Security, Severity.Warning, $"{launch.Card.Name} no está en Tools/{launch.Card.Id}/", source, launch.Message);
        }

        if (launch.ExitCode is null)
        {
            return Finding.Create(Sections.Security, Severity.Fail, $"{launch.Card.Name} no se ejecutó", source, launch.Message);
        }

        var severity = launch.ExitCode == 0 ? Severity.Ok : Severity.Warning;
        return Finding.Create(
            Sections.Security,
            severity,
            $"{launch.Card.Name} lanzado",
            source,
            launch.Message);
    }

    private static string? VerifyIntegrity(string toolsDirectory, ToolCard card, string path)
    {
        if (!Integrity.IsHexSha256(card.Sha256))
        {
            return null;
        }

        var folder = ManifestStore.ToolFolder(toolsDirectory, card.Id);
        var stamp = InstallRecord.Read(folder);
        if (ToolUpdater.IsZipDownload(card.Download))
        {
            if (stamp is null)
            {
                Integrity.Log(toolsDirectory, $"BLOCK {card.Id}: binario local sin installed.json; el hash del manifiesto es del zip.");
                return "El sha256 del manifiesto es del zip. Instala con Actualizar o no se ejecuta un binario suelto.";
            }

            if (!string.Equals(stamp.SourceSha256, card.Sha256, StringComparison.OrdinalIgnoreCase))
            {
                Integrity.Log(toolsDirectory, $"BLOCK {card.Id}: zip instalado {stamp.SourceSha256} ≠ manifiesto {card.Sha256}.");
                return "El paquete instalado no coincide con el sha256 del manifiesto. No se ejecuta.";
            }

            if (!Integrity.IsHexSha256(stamp.ExecutableSha256) || !Integrity.Matches(path, stamp.ExecutableSha256))
            {
                Integrity.Log(toolsDirectory, $"BLOCK {card.Id}: el ejecutable no coincide con el hash registrado.");
                return "El ejecutable fue modificado. No se ejecuta.";
            }

            return null;
        }

        if (!Integrity.Matches(path, card.Sha256))
        {
            Integrity.Log(toolsDirectory, $"BLOCK {card.Id}: sha256 del exe no coincide.");
            return "sha256 no coincide. No se ejecuta.";
        }

        return null;
    }

    private static string? FindInDirectory(string directory, IReadOnlyList<string> names, bool recursive)
    {
        if (!Directory.Exists(directory))
        {
            return null;
        }

        foreach (var name in names)
        {
            var direct = Path.Combine(directory, name);
            if (File.Exists(direct))
            {
                return direct;
            }
        }

        if (!recursive)
        {
            return null;
        }

        try
        {
            return Directory.EnumerateFiles(directory, "*.exe", SearchOption.AllDirectories)
                .FirstOrDefault(path => names.Any(n =>
                    string.Equals(Path.GetFileName(path), n, StringComparison.OrdinalIgnoreCase)));
        }
        catch
        {
            return null;
        }
    }

    private static string Quote(string argument) =>
        argument.Contains(' ') ? "\"" + argument.Replace("\"", "\\\"") + "\"" : argument;
}
