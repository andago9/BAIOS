using System.Text.Json;

namespace BAIOS.Tools;

public sealed class RescueDocument
{
    public int SchemaVersion { get; set; } = 1;
    public List<RescueItem> Items { get; set; } = [];
}

public sealed class RescueItem
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Url { get; set; } = "";
    public string Note { get; set; } = "";
}

public static class RescueCatalog
{
    public const string FileName = "rescue.json";
    public const int SchemaVersion = 1;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static IReadOnlyList<RescueItem> Load(string? baseDirectory = null)
    {
        var path = Path.Combine(baseDirectory ?? AppContext.BaseDirectory, FileName);
        if (File.Exists(path))
        {
            try
            {
                var document = JsonSerializer.Deserialize<RescueDocument>(File.ReadAllText(path), JsonOptions);
                if (document is not null && document.SchemaVersion == SchemaVersion)
                {
                    return document.Items.Where(Valid).ToList();
                }
            }
            catch (Exception ex)
            {
                BAIOS.Core.AppLog.Error("No se pudo leer rescue.json.", ex);
            }
        }

        return Default();
    }

    public static IReadOnlyList<RescueItem> Default() =>
    [
        new RescueItem
        {
            Id = "kaspersky-rescue",
            Name = "Kaspersky Rescue Disk",
            Url = "https://www.kaspersky.com/downloads/free-rescue-disk",
            Note = "Página oficial. En EE. UU. la descarga puede no estar disponible. BAIOS no descarga la ISO."
        },
        new RescueItem
        {
            Id = "drweb-livedisk",
            Name = "Dr.Web LiveDisk",
            Url = "https://free.drweb.com/aid_admin",
            Note = "Recuperación de emergencia en DVD o USB. BAIOS no descarga la ISO."
        },
        new RescueItem
        {
            Id = "avira-rescue",
            Name = "Avira Rescue System",
            Url = "https://support.avira.com/hc/en-us/articles/360007776058-Creating-and-using-Avira-Rescue-System",
            Note = "Artículo de soporte oficial. BAIOS no descarga la ISO."
        },
        new RescueItem
        {
            Id = "vba32-rescue",
            Name = "VBA32 Rescue",
            Url = "https://www.anti-virus.by/en/",
            Note = "VirusBlokAda. Solo el sitio del fabricante. BAIOS no descarga la ISO."
        }
    ];

    private static bool Valid(RescueItem item) =>
        !string.IsNullOrWhiteSpace(item.Name) && ManifestStore.IsHttpsUrl(item.Url);
}
