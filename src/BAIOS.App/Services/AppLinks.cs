using System.Diagnostics;
using System.IO;

namespace BAIOS.App.Services;

public static class AppLinks
{
    public const string Website = "https://github.com/andago9/BAIOS";
    public const string Forum = "https://github.com/andago9/BAIOS/issues";

    public static void OpenUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return;
        }

        Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
    }

    public static void OpenLicense()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "LICENSE");
        if (!File.Exists(path))
        {
            path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "LICENSE"));
        }

        if (!File.Exists(path))
        {
            return;
        }

        Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
    }

    public static void OpenUserGuide()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "usuario.md");
        if (!File.Exists(path))
        {
            path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "docs", "usuario.md"));
        }

        if (File.Exists(path))
        {
            Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
            return;
        }

        OpenUrl(Website + "/blob/main/docs/usuario.md");
    }
}
