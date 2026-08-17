using System.IO.Compression;
using System.Text.Json;

namespace BAIOS.Tools;

public sealed class ToolInstallState
{
    public bool HasBinary { get; init; }
    public string? InstalledVersion { get; init; }
    public bool NeedsUpdate { get; init; }
    public bool CanDownload { get; init; }
    public string Status { get; init; } = "";
}

public sealed class UpdateResult
{
    public bool Success { get; init; }
    public string Message { get; init; } = "";
    public string? Path { get; init; }
}

public static class ToolUpdater
{
    private const long MaxBytes = 200L * 1024 * 1024;
    private static readonly HttpClient Http = CreateClient();

    public static bool IsZipDownload(string downloadUrl) =>
        Uri.TryCreate(downloadUrl, UriKind.Absolute, out var uri)
        && uri.AbsolutePath.EndsWith(".zip", StringComparison.OrdinalIgnoreCase);

    public static ToolInstallState Inspect(string toolsDirectory, ToolCard card)
    {
        var folder = ManifestStore.ToolFolder(toolsDirectory, card.Id);
        var path = ToolsModule.FindTool(toolsDirectory, card);
        var stamp = InstallRecord.Read(folder);
        var hasHash = Integrity.IsHexSha256(card.Sha256);
        var https = ManifestStore.IsHttpsUrl(card.Download);
        var canDownload = hasHash && https;
        var needsUpdate = path is null
            || stamp is null
            || !string.Equals(stamp.Version, card.Version, StringComparison.OrdinalIgnoreCase)
            || (hasHash && !string.Equals(stamp.SourceSha256, card.Sha256, StringComparison.OrdinalIgnoreCase));

        string status;
        if (path is null)
        {
            status = canDownload
                ? "No instalada. Puedes descargarla desde el manifiesto."
                : $"Coloca el binario en Tools/{card.Id}/ o publica sha256 https en el manifiesto para descargar.";
        }
        else if (!hasHash)
        {
            status = $"Local: {path}. Sin sha256 en el manifiesto: no se verifica ni se descarga.";
        }
        else if (needsUpdate)
        {
            status = $"Hay versión de manifiesto {card.Version}. Local: {stamp?.Version ?? "desconocida"}.";
        }
        else
        {
            status = $"Instalada v{stamp?.Version ?? card.Version} (hash verificado al instalar).";
        }

        return new ToolInstallState
        {
            HasBinary = path is not null,
            InstalledVersion = stamp?.Version,
            NeedsUpdate = needsUpdate && canDownload,
            CanDownload = canDownload,
            Status = status
        };
    }

    public static async Task<UpdateResult> UpdateAsync(
        string toolsDirectory,
        ToolCard card,
        CancellationToken cancellationToken = default)
    {
        if (!ManifestStore.IsHttpsUrl(card.Download))
        {
            return Fail(toolsDirectory, card, "La URL de descarga no es https.");
        }

        if (!Integrity.IsHexSha256(card.Sha256))
        {
            return Fail(toolsDirectory, card, "El manifiesto no tiene sha256 (64 hex). No se descarga.");
        }

        var tmpRoot = Path.Combine(toolsDirectory, ".tmp");
        Directory.CreateDirectory(tmpRoot);
        var downloadPath = Path.Combine(tmpRoot, card.Id + "-" + Guid.NewGuid().ToString("N"));
        try
        {
            var sourceName = await DownloadHttpsAsync(card.Download, downloadPath, allowJson: false, MaxBytes, cancellationToken).ConfigureAwait(false);
            if (!Integrity.Matches(downloadPath, card.Sha256))
            {
                var actual = Integrity.Sha256Hex(downloadPath);
                Integrity.Log(toolsDirectory, $"FAIL hash {card.Id}: esperado {card.Sha256}, actual {actual}. No se instala.");
                return new UpdateResult { Success = false, Message = "sha256 no coincide. No se instaló ni se ejecutará este archivo." };
            }

            var folder = ManifestStore.ToolFolder(toolsDirectory, card.Id);
            var staging = folder + ".new";
            if (Directory.Exists(staging))
            {
                Directory.Delete(staging, true);
            }

            Directory.CreateDirectory(staging);
            string? exePath;
            if (IsZipDownload(card.Download) || sourceName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
            {
                exePath = ExtractExecutable(downloadPath, staging, card);
            }
            else
            {
                var destName = card.FileNames.FirstOrDefault() ?? (card.Id + ".exe");
                exePath = Path.Combine(staging, destName);
                File.Copy(downloadPath, exePath, overwrite: true);
            }

            if (exePath is null || !File.Exists(exePath))
            {
                return Fail(toolsDirectory, card, "El paquete no contiene el ejecutable declarado.");
            }

            var stamp = new InstalledStamp
            {
                Version = card.Version,
                SourceSha256 = card.Sha256,
                ExecutableSha256 = Integrity.Sha256Hex(exePath),
                SourceName = sourceName,
                InstalledAt = DateTimeOffset.Now
            };
            InstallRecord.Write(staging, stamp);

            ReplaceFolder(folder, staging);
            Integrity.Log(toolsDirectory, $"OK instalado {card.Id} v{card.Version} sha256={card.Sha256}");
            var finalPath = ToolsModule.FindTool(toolsDirectory, card);
            return new UpdateResult
            {
                Success = true,
                Path = finalPath,
                Message = $"Instalado {card.Name} v{card.Version} en Tools/{card.Id}/."
            };
        }
        catch (Exception ex)
        {
            return Fail(toolsDirectory, card, ex.Message);
        }
        finally
        {
            TryDelete(downloadPath);
        }
    }

    public static async Task<UpdateResult> RefreshManifestAsync(
        string manifestUrl,
        string localManifestPath,
        CancellationToken cancellationToken = default)
    {
        if (!ManifestStore.IsHttpsUrl(manifestUrl))
        {
            return new UpdateResult { Success = false, Message = "manifestUrl debe ser https." };
        }

        var tmp = localManifestPath + ".tmp";
        try
        {
            await DownloadHttpsToFileAsync(manifestUrl, tmp, allowJson: true, MaxBytes, cancellationToken).ConfigureAwait(false);
            var json = await File.ReadAllTextAsync(tmp, cancellationToken).ConfigureAwait(false);
            var loaded = ManifestStore.Parse(json, localManifestPath);
            if (loaded.Document.SchemaVersion != ManifestStore.SchemaVersion)
            {
                return new UpdateResult { Success = false, Message = "El manifiesto remoto no es schema 1." };
            }

            File.Copy(tmp, localManifestPath, overwrite: true);
            return new UpdateResult
            {
                Success = true,
                Path = localManifestPath,
                Message = $"Manifiesto actualizado ({loaded.Document.Tools.Count} ficha(s))."
            };
        }
        catch (Exception ex)
        {
            return new UpdateResult { Success = false, Message = "No se pudo actualizar el manifiesto: " + ex.Message };
        }
        finally
        {
            TryDelete(tmp);
        }
    }

    public static async Task<UpdateResult> RefreshEngineAsync(
        string engineUrl,
        string localEnginePath,
        CancellationToken cancellationToken = default)
    {
        if (!ManifestStore.IsHttpsUrl(engineUrl))
        {
            return new UpdateResult { Success = false, Message = "engineUrl debe ser https." };
        }

        var tmp = localEnginePath + ".tmp";
        try
        {
            await DownloadHttpsToFileAsync(engineUrl, tmp, allowJson: true, MaxBytes, cancellationToken).ConfigureAwait(false);
            var json = await File.ReadAllTextAsync(tmp, cancellationToken).ConfigureAwait(false);
            var document = JsonSerializer.Deserialize<EngineDocument>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (document is null || (document.SchemaVersion != 1 && document.SchemaVersion != 0))
            {
                return new UpdateResult { Success = false, Message = "El engine.json remoto no es schema 1." };
            }

            File.Copy(tmp, localEnginePath, overwrite: true);
            return new UpdateResult
            {
                Success = true,
                Path = localEnginePath,
                Message = "engine.json actualizado (v" + document.Version + ")."
            };
        }
        catch (Exception ex)
        {
            return new UpdateResult { Success = false, Message = "No se pudo actualizar engine.json: " + ex.Message };
        }
        finally
        {
            TryDelete(tmp);
        }
    }

    public static Task<string> DownloadHttpsToFileAsync(
        string url,
        string destination,
        bool allowJson,
        long maxBytes,
        CancellationToken cancellationToken) =>
        DownloadHttpsAsync(url, destination, allowJson, maxBytes, cancellationToken);

    private static UpdateResult Fail(string toolsDirectory, ToolCard card, string message)
    {
        Integrity.Log(toolsDirectory, $"FAIL {card.Id}: {message}");
        return new UpdateResult { Success = false, Message = message };
    }

    private static HttpClient CreateClient()
    {
        var handler = new HttpClientHandler { AllowAutoRedirect = false };
        var client = new HttpClient(handler) { Timeout = TimeSpan.FromMinutes(5) };
        client.DefaultRequestHeaders.UserAgent.ParseAdd("BAIOS/4.0 (tool-updater)");
        return client;
    }

    private static async Task<string> DownloadHttpsAsync(
        string url,
        string destination,
        bool allowJson,
        long maxBytes,
        CancellationToken cancellationToken)
    {
        var current = url;
        for (var hop = 0; hop < 8; hop++)
        {
            if (!ManifestStore.IsHttpsUrl(current))
            {
                throw new InvalidOperationException("Redirección no https. Abortado.");
            }

            using var request = new HttpRequestMessage(HttpMethod.Get, current);
            using var response = await Http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            if ((int)response.StatusCode is >= 300 and < 400)
            {
                var location = response.Headers.Location ?? throw new InvalidOperationException("Redirección sin Location.");
                current = location.IsAbsoluteUri ? location.ToString() : new Uri(new Uri(current), location).ToString();
                continue;
            }

            response.EnsureSuccessStatusCode();
            var media = response.Content.Headers.ContentType?.MediaType ?? "";
            if (media.Contains("html", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("La URL devolvió HTML, no un archivo descargable. Usa una descarga directa https en el manifiesto.");
            }

            if (!allowJson && media.Contains("json", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("La URL devolvió JSON, no el binario de la herramienta.");
            }

            var length = response.Content.Headers.ContentLength;
            if (length > maxBytes)
            {
                throw new InvalidOperationException("El archivo supera el tamaño máximo permitido.");
            }

            await using (var input = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false))
            await using (var output = File.Create(destination))
            {
                var buffer = new byte[81920];
                long total = 0;
                int read;
                while ((read = await input.ReadAsync(buffer, cancellationToken).ConfigureAwait(false)) > 0)
                {
                    total += read;
                    if (total > maxBytes)
                    {
                        throw new InvalidOperationException("El archivo supera el tamaño máximo permitido.");
                    }

                    await output.WriteAsync(buffer.AsMemory(0, read), cancellationToken).ConfigureAwait(false);
                }
            }

            var name = response.Content.Headers.ContentDisposition?.FileNameStar
                       ?? response.Content.Headers.ContentDisposition?.FileName
                       ?? Path.GetFileName(new Uri(current).AbsolutePath);
            return string.IsNullOrWhiteSpace(name) ? Path.GetFileName(destination) : name.Trim('"');
        }

        throw new InvalidOperationException("Demasiadas redirecciones.");
    }

    private static string? ExtractExecutable(string zipPath, string staging, ToolCard card)
    {
        using var zip = System.IO.Compression.ZipFile.OpenRead(zipPath);
        string? found = null;
        foreach (var entry in zip.Entries)
        {
            if (string.IsNullOrEmpty(entry.Name) || entry.FullName.Contains("..", StringComparison.Ordinal))
            {
                continue;
            }

            var fileName = Path.GetFileName(entry.FullName);
            if (!card.FileNames.Any(n => string.Equals(n, fileName, StringComparison.OrdinalIgnoreCase)))
            {
                continue;
            }

            var dest = Path.Combine(staging, fileName);
            entry.ExtractToFile(dest, overwrite: true);
            if (string.Equals(fileName, card.FileNames[0], StringComparison.OrdinalIgnoreCase) || found is null)
            {
                found = dest;
            }
        }

        return found;
    }

    private static void ReplaceFolder(string folder, string staging)
    {
        var backup = folder + ".bak";
        if (Directory.Exists(backup))
        {
            Directory.Delete(backup, true);
        }

        if (Directory.Exists(folder))
        {
            Directory.Move(folder, backup);
        }

        Directory.Move(staging, folder);
        if (Directory.Exists(backup))
        {
            try
            {
                Directory.Delete(backup, true);
            }
            catch
            {
            }
        }
    }

    private static void TryDelete(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch
        {
        }
    }
}
