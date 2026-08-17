namespace BAIOS.Tools;

public sealed class ToolCard
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Vendor { get; init; }
    public required string Url { get; init; }
    public required string LicenseUrl { get; init; }
    public required string Architecture { get; init; }
    public required bool RequiresAdmin { get; init; }
    public required string HomeLabel { get; init; }
    public string? Note { get; init; }
    public IReadOnlyList<string> FileNames { get; init; } = [];
    public string Version { get; init; } = "";
    public string Category { get; init; } = "other";
    public string Download { get; init; } = "";
    public string Sha256 { get; init; } = "";
    public IReadOnlyList<string> Arguments { get; init; } = [];
    public int? ExpiresDays { get; init; }
    public int? TrialDays { get; init; }
    public string? ManualUrl { get; init; }
    public bool Portable { get; init; } = true;
    public bool FalsePositiveWarning { get; init; }
    public string? Icon { get; init; }

    public static ToolCard FromManifest(ManifestTool tool)
    {
        var executable = string.IsNullOrWhiteSpace(tool.Executable) ? tool.Id + ".exe" : tool.Executable;
        var names = new List<string> { executable };
        if (string.Equals(tool.Id, "autoruns", StringComparison.OrdinalIgnoreCase))
        {
            names.AddRange(["Autoruns64.exe", "autoruns64.exe", "Autoruns.exe", "autoruns.exe"]);
        }
        else if (string.Equals(tool.Id, "msert", StringComparison.OrdinalIgnoreCase))
        {
            names.AddRange(["msert.exe", "MSERT.exe"]);
        }
        else if (string.Equals(tool.Id, "hijackthis", StringComparison.OrdinalIgnoreCase))
        {
            names.AddRange(["HiJackThis.exe", "HijackThis.exe", "hijackthis.exe"]);
        }
        else if (string.Equals(tool.Id, "kvrt", StringComparison.OrdinalIgnoreCase))
        {
            names.AddRange(["KVRT.exe", "kvrt.exe"]);
        }
        else if (string.Equals(tool.Id, "hitmanpro", StringComparison.OrdinalIgnoreCase))
        {
            names.AddRange(["HitmanPro_x64.exe", "HitmanPro.exe"]);
        }
        else if (string.Equals(tool.Id, "zhpcleaner", StringComparison.OrdinalIgnoreCase))
        {
            names.AddRange(["ZHPCleaner.exe"]);
        }
        else if (string.Equals(tool.Id, "eek", StringComparison.OrdinalIgnoreCase))
        {
            names.AddRange(["EmsisoftEmergencyKit.exe", "EEK.exe"]);
        }

        return new ToolCard
        {
            Id = tool.Id,
            Name = tool.Name,
            Vendor = tool.Publisher,
            Url = string.IsNullOrWhiteSpace(tool.ManualUrl) ? tool.HomeUrl : tool.ManualUrl,
            LicenseUrl = string.IsNullOrWhiteSpace(tool.EulaUrl) ? tool.LicenseUrl : tool.EulaUrl,
            Architecture = tool.Architecture,
            RequiresAdmin = tool.RequiresAdmin,
            HomeLabel = HomeLabelFor(tool.Id, tool.Name),
            Note = tool.ResultHints,
            FileNames = names.Distinct(StringComparer.OrdinalIgnoreCase).ToList(),
            Version = tool.Version,
            Category = tool.Category,
            Download = tool.Download,
            Sha256 = tool.Sha256,
            Arguments = tool.Arguments,
            ExpiresDays = tool.ExpiresDays,
            TrialDays = tool.TrialDays,
            ManualUrl = tool.ManualUrl,
            Portable = tool.Portable,
            FalsePositiveWarning = tool.FalsePositiveWarning,
            Icon = tool.Icon
        };
    }

    private static string HomeLabelFor(string id, string name) => id switch
    {
        "adwcleaner" => "Ejecutar limpieza de adware",
        "msert" => "Segunda opinión Microsoft",
        "autoruns" => "Revisar programas de inicio",
        "hijackthis" => "Ejecutar HijackThis",
        _ => "Ejecutar " + name
    };
}

public sealed class ToolLaunch
{
    public required ToolCard Card { get; init; }
    public string? Path { get; init; }
    public bool Found => Path is not null;
    public DateTimeOffset? LaunchedAt { get; init; }
    public int? ExitCode { get; init; }
    public string? Message { get; init; }
}

public sealed class ToolCatalog
{
    public IReadOnlyList<ToolCard> Tools { get; init; } = [];
    public string ManifestPath { get; init; } = "";
    public bool FromFile { get; init; }
    public IReadOnlyList<string> Warnings { get; init; } = [];
}
