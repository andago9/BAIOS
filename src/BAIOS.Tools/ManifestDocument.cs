namespace BAIOS.Tools;

public sealed class ManifestDocument
{
    public int SchemaVersion { get; set; } = 1;
    public List<ManifestTool> Tools { get; set; } = [];
}

public sealed class ManifestTool
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Version { get; set; } = "";
    public string Architecture { get; set; } = "x64";
    public string Download { get; set; } = "";
    public string Sha256 { get; set; } = "";
    public string Category { get; set; } = "other";
    public string Publisher { get; set; } = "";
    public string LicenseUrl { get; set; } = "";
    public string HomeUrl { get; set; } = "";
    public bool RequiresAdmin { get; set; }
    public List<string> Arguments { get; set; } = [];
    public string ResultHints { get; set; } = "";
    public string? ManualUrl { get; set; }
    public string? EulaUrl { get; set; }
    public string? Executable { get; set; }
    public int? ExpiresDays { get; set; }
    public int? TrialDays { get; set; }
    public bool Portable { get; set; } = true;
    public bool FalsePositiveWarning { get; set; }
    public string? Icon { get; set; }
}
