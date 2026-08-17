using System.Text;

namespace BAIOS.Tools;

public static class ExecutionLog
{
    public const string FileName = "execution.log";

    public static string PathFor(string toolsDirectory, string id) =>
        Path.Combine(ManifestStore.ToolFolder(toolsDirectory, id), FileName);

    public static void Append(
        string toolsDirectory,
        string id,
        string message,
        string? path = null,
        string? args = null,
        int? exitCode = null,
        TimeSpan? duration = null)
    {
        try
        {
            var folder = ManifestStore.ToolFolder(toolsDirectory, id);
            Directory.CreateDirectory(folder);
            var line = $"{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss zzz} {id}";
            if (!string.IsNullOrWhiteSpace(path))
            {
                line += $" path={path}";
            }

            if (!string.IsNullOrWhiteSpace(args))
            {
                line += $" args={args}";
            }

            if (exitCode is not null)
            {
                line += $" exit={exitCode}";
            }

            if (duration is not null)
            {
                line += $" duration={duration.Value.TotalSeconds:0.###}s";
            }

            line += " " + message + Environment.NewLine;
            File.AppendAllText(PathFor(toolsDirectory, id), line, Encoding.UTF8);
        }
        catch
        {
        }
    }
}
