using System.Diagnostics;
using System.IO.Compression;
using System.Text.Json;
using BAIOS.Core;

namespace BAIOS.Tools;

public sealed class EngineDocument
{
    public int SchemaVersion { get; set; } = 1;
    public string Version { get; set; } = "";
    public string Download { get; set; } = "";
    public string Sha256 { get; set; } = "";
}

public static class EngineUpdater
{
    public const string FileName = "engine.json";
    public const string PendingFolder = "update-pending";
    public const int SchemaVersion = 1;
    private const long MaxBytes = 400L * 1024 * 1024;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static string PathBesideExe(string? baseDirectory = null) =>
        Path.Combine(baseDirectory ?? AppContext.BaseDirectory, FileName);

    public static EngineDocument Load(string? baseDirectory = null)
    {
        var path = PathBesideExe(baseDirectory);
        if (!File.Exists(path))
        {
            return new EngineDocument { Version = "4.0.0-dev" };
        }

        try
        {
            return JsonSerializer.Deserialize<EngineDocument>(File.ReadAllText(path), JsonOptions)
                   ?? new EngineDocument { Version = "4.0.0-dev" };
        }
        catch (Exception ex)
        {
            AppLog.Error("No se pudo leer engine.json.", ex);
            return new EngineDocument { Version = "4.0.0-dev" };
        }
    }

    public static bool CanDownload(EngineDocument engine) =>
        ManifestStore.IsHttpsUrl(engine.Download) && Integrity.IsHexSha256(engine.Sha256);

    public static bool TryRelaunchToApplyPending(string? baseDirectory = null)
    {
        var dest = Path.GetFullPath(baseDirectory ?? AppContext.BaseDirectory);
        var pending = Path.Combine(dest, PendingFolder);
        var ready = Path.Combine(pending, ".ready");
        if (!File.Exists(ready))
        {
            return false;
        }

        var exe = Path.Combine(dest, "BAIOS.exe");
        var cmd = Path.Combine(Path.GetTempPath(), "baios-apply-update.cmd");
        var script =
            "@echo off" + Environment.NewLine +
            "ping 127.0.0.1 -n 3 >nul" + Environment.NewLine +
            $"robocopy \"{pending}\" \"{dest}\" /E /NFL /NDL /NJH /NJS /nc /ns /np" + Environment.NewLine +
            $"rmdir /s /q \"{pending}\"" + Environment.NewLine +
            $"start \"\" \"{exe}\"" + Environment.NewLine;
        File.WriteAllText(cmd, script);
        AppLog.Info("Aplicando actualización pendiente del motor.");
        Process.Start(new ProcessStartInfo
        {
            FileName = cmd,
            UseShellExecute = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        });
        return true;
    }

    public static async Task<UpdateResult> UpdateAsync(
        string? engineUrl,
        string? baseDirectory = null,
        CancellationToken cancellationToken = default)
    {
        var dir = baseDirectory ?? AppContext.BaseDirectory;
        var local = PathBesideExe(dir);
        if (ManifestStore.IsHttpsUrl(engineUrl))
        {
            var refreshed = await ToolUpdater.RefreshEngineAsync(engineUrl!, local, cancellationToken).ConfigureAwait(false);
            if (!refreshed.Success)
            {
                AppLog.Error("No se pudo refrescar engine.json: " + refreshed.Message);
                return refreshed;
            }
        }

        var engine = Load(dir);
        if (engine.SchemaVersion != SchemaVersion && engine.SchemaVersion != 0)
        {
            return new UpdateResult { Success = false, Message = "engine.json no es schema 1." };
        }

        if (!CanDownload(engine))
        {
            return new UpdateResult
            {
                Success = false,
                Message = "Para actualizar el motor hace falta URL https y sha256 (64 hex) en engine.json."
            };
        }

        var tmp = Path.Combine(Path.GetTempPath(), "baios-engine-" + Guid.NewGuid().ToString("N") + ".zip");
        var staging = Path.Combine(dir, PendingFolder + ".new");
        try
        {
            await ToolUpdater.DownloadHttpsToFileAsync(engine.Download, tmp, allowJson: false, MaxBytes, cancellationToken)
                .ConfigureAwait(false);
            if (!Integrity.Matches(tmp, engine.Sha256))
            {
                AppLog.Error($"Hash del motor incorrecto. Esperado {engine.Sha256}.");
                return new UpdateResult { Success = false, Message = "sha256 no coincide. No se aplicará la actualización." };
            }

            if (Directory.Exists(staging))
            {
                Directory.Delete(staging, true);
            }

            Directory.CreateDirectory(staging);
            ZipFile.ExtractToDirectory(tmp, staging);
            UnwrapSingleFolder(staging);
            if (!File.Exists(Path.Combine(staging, "BAIOS.exe")))
            {
                return new UpdateResult { Success = false, Message = "El zip del motor no contiene BAIOS.exe." };
            }

            var pending = Path.Combine(dir, PendingFolder);
            if (Directory.Exists(pending))
            {
                Directory.Delete(pending, true);
            }

            Directory.Move(staging, pending);
            File.WriteAllText(Path.Combine(pending, ".ready"), engine.Version);
            AppLog.Info("Actualización del motor lista (v" + engine.Version + "). Se aplica al reiniciar.");
            return new UpdateResult
            {
                Success = true,
                Message = "Descarga verificada. Cierra BAIOS y vuelve a abrirlo para aplicar la actualización."
            };
        }
        catch (Exception ex)
        {
            AppLog.Error("Falló la actualización del motor.", ex);
            return new UpdateResult { Success = false, Message = ex.Message };
        }
        finally
        {
            try
            {
                if (File.Exists(tmp))
                {
                    File.Delete(tmp);
                }
            }
            catch
            {
            }

            try
            {
                if (Directory.Exists(staging))
                {
                    Directory.Delete(staging, true);
                }
            }
            catch
            {
            }
        }
    }

    private static void UnwrapSingleFolder(string staging)
    {
        var entries = Directory.GetFileSystemEntries(staging);
        if (entries.Length != 1 || !Directory.Exists(entries[0]))
        {
            return;
        }

        if (!File.Exists(Path.Combine(entries[0], "BAIOS.exe")))
        {
            return;
        }

        foreach (var child in Directory.GetFileSystemEntries(entries[0]))
        {
            var name = Path.GetFileName(child);
            var dest = Path.Combine(staging, name);
            if (Directory.Exists(child))
            {
                Directory.Move(child, dest);
            }
            else
            {
                File.Move(child, dest, overwrite: true);
            }
        }

        Directory.Delete(entries[0], true);
    }
}
