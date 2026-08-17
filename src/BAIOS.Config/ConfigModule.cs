using BAIOS.Core;
using System.IO;

namespace BAIOS.Config;

public static class ConfigModule
{
    public static AppConfig Load() => ConfigStore.Load();

    public static AppMode DefaultMode(AppConfig config) => ConfigStore.ResolveDefaultMode(config);

    public static string ToolsDirectory(AppConfig config) => ConfigStore.ResolvePath(config.ToolsPath);

    public static string ReportsDirectory(AppConfig config) => ConfigStore.ResolvePath(config.ReportsPath);

    public static string ManifestPath(string? baseDirectory = null) =>
        Path.Combine(baseDirectory ?? AppContext.BaseDirectory, "manifest.json");
}
