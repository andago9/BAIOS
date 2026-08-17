using System.Text.Json;

namespace BAIOS.Tools;

public sealed class InstalledStamp
{
    public string Version { get; set; } = "";
    public string SourceSha256 { get; set; } = "";
    public string ExecutableSha256 { get; set; } = "";
    public string SourceName { get; set; } = "";
    public DateTimeOffset InstalledAt { get; set; }
}

public static class InstallRecord
{
    public const string FileName = "installed.json";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static string PathFor(string toolFolder) => Path.Combine(toolFolder, FileName);

    public static InstalledStamp? Read(string toolFolder)
    {
        var path = PathFor(toolFolder);
        if (!File.Exists(path))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<InstalledStamp>(File.ReadAllText(path), JsonOptions);
        }
        catch
        {
            return null;
        }
    }

    public static void Write(string toolFolder, InstalledStamp stamp)
    {
        Directory.CreateDirectory(toolFolder);
        File.WriteAllText(PathFor(toolFolder), JsonSerializer.Serialize(stamp, JsonOptions));
    }
}
