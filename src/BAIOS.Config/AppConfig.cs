namespace BAIOS.Config;

public sealed class AppConfig
{
    public string ModeDefault { get; set; } = "Home";
    public string ToolsPath { get; set; } = "Tools";
    public string ReportsPath { get; set; } = "Reports";
    public string? ManifestUrl { get; set; }
    public string? EngineUrl { get; set; }
}
