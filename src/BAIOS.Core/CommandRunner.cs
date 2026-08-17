using System.ComponentModel;
using System.Diagnostics;

namespace BAIOS.Core;

public static class CommandRunner
{
    public static async Task<CommandResult> RunAsync(
        string fileName,
        string arguments,
        bool requireAdmin = false,
        bool captureOutput = true,
        CancellationToken cancellationToken = default)
    {
        var alreadyAdmin = Elevation.IsAdministrator;
        var useRunas = requireAdmin && !alreadyAdmin;

        var psi = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            UseShellExecute = useRunas,
            CreateNoWindow = !useRunas,
            WorkingDirectory = Path.GetDirectoryName(fileName) is { Length: > 0 } dir ? dir : Environment.SystemDirectory
        };

        if (useRunas)
        {
            psi.Verb = "runas";
        }
        else if (captureOutput)
        {
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
        }

        try
        {
            using var process = Process.Start(psi);
            if (process is null)
            {
                return new CommandResult
                {
                    Success = false,
                    Message = "No se pudo iniciar el proceso."
                };
            }

            var stdout = "";
            var stderr = "";
            if (!useRunas && captureOutput)
            {
                var outTask = process.StandardOutput.ReadToEndAsync(cancellationToken);
                var errTask = process.StandardError.ReadToEndAsync(cancellationToken);
                await process.WaitForExitAsync(cancellationToken).ConfigureAwait(false);
                stdout = await outTask.ConfigureAwait(false);
                stderr = await errTask.ConfigureAwait(false);
            }
            else
            {
                await process.WaitForExitAsync(cancellationToken).ConfigureAwait(false);
            }

            return new CommandResult
            {
                ExitCode = process.ExitCode,
                StandardOutput = stdout,
                StandardError = stderr,
                Elevated = alreadyAdmin || useRunas,
                Success = process.ExitCode == 0
            };
        }
        catch (Win32Exception ex) when (ex.NativeErrorCode == 1223)
        {
            return new CommandResult
            {
                Success = false,
                Cancelled = true,
                Message = "UAC cancelado. No se ejecutó la acción."
            };
        }
        catch (Exception ex)
        {
            return new CommandResult
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public static ProcessStartInfo LaunchInfo(string fileName, string arguments = "", bool requireAdmin = false)
    {
        var alreadyAdmin = Elevation.IsAdministrator;
        var useRunas = requireAdmin && !alreadyAdmin;
        return new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            UseShellExecute = true,
            Verb = useRunas ? "runas" : string.Empty,
            WorkingDirectory = Path.GetDirectoryName(fileName) is { Length: > 0 } dir ? dir : AppContext.BaseDirectory
        };
    }
}
