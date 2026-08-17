using System.Text.Json;
using BAIOS.Core;

namespace BAIOS.Config;

public static class ConfigStore
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true
    };

    public static AppConfig Load(string? baseDirectory = null)
    {
        var dir = baseDirectory ?? AppContext.BaseDirectory;
        var path = Path.Combine(dir, "config.json");
        if (!File.Exists(path))
        {
            return new AppConfig();
        }

        try
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<AppConfig>(json, JsonOptions) ?? new AppConfig();
        }
        catch
        {
            return new AppConfig();
        }
    }

    public static string ResolvePath(string relativeOrAbsolute, string? baseDirectory = null)
    {
        var dir = baseDirectory ?? AppContext.BaseDirectory;
        return Path.IsPathRooted(relativeOrAbsolute)
            ? relativeOrAbsolute
            : Path.GetFullPath(Path.Combine(dir, relativeOrAbsolute));
    }

    public static bool IsRunningFromRemovable()
    {
        try
        {
            var root = Path.GetPathRoot(Path.GetFullPath(AppContext.BaseDirectory));
            if (string.IsNullOrEmpty(root))
            {
                return false;
            }

            return new DriveInfo(root).DriveType == DriveType.Removable;
        }
        catch
        {
            return false;
        }
    }

    public static AppMode ResolveDefaultMode(AppConfig config)
    {
        if (IsRunningFromRemovable())
        {
            return AppMode.Technician;
        }

        return string.Equals(config.ModeDefault, "Technician", StringComparison.OrdinalIgnoreCase)
            ? AppMode.Technician
            : AppMode.Home;
    }
}
