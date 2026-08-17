using System.Text;

namespace BAIOS.Core;

public static class AppLog
{
    public const string FileName = "app.log";

    public static string DirectoryPath(string? baseDirectory = null) =>
        Path.Combine(baseDirectory ?? AppContext.BaseDirectory, "logs");

    public static string PathFor(string? baseDirectory = null) =>
        Path.Combine(DirectoryPath(baseDirectory), FileName);

    public static void Info(string message) => Write("INFO", message);

    public static void Error(string message, Exception? ex = null) =>
        Write("ERROR", ex is null ? message : message + " " + ex);

    public static void Write(string level, string message, string? baseDirectory = null)
    {
        try
        {
            Directory.CreateDirectory(DirectoryPath(baseDirectory));
            var line = $"{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss zzz} {level} {message}{Environment.NewLine}";
            File.AppendAllText(PathFor(baseDirectory), line, Encoding.UTF8);
        }
        catch
        {
        }
    }
}
