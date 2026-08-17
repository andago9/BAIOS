namespace BAIOS.Security;

public enum DefenderPresence
{
    Active,
    Passive,
    Absent,
    Unknown
}

public sealed class DefenderStatus
{
    public DefenderPresence Presence { get; init; } = DefenderPresence.Unknown;
    public bool RealTimeProtection { get; init; }
    public bool ServiceEnabled { get; init; }
    public string? LastQuickScan { get; init; }
    public string? LastFullScan { get; init; }
    public int? QuickScanAgeDays { get; init; }
    public int? FullScanAgeDays { get; init; }
    public string Summary { get; init; } = "No se pudo leer Microsoft Defender.";
}

public sealed class FirewallStatus
{
    public bool DomainEnabled { get; init; }
    public bool PrivateEnabled { get; init; }
    public bool PublicEnabled { get; init; }
    public bool AnyEnabled => DomainEnabled || PrivateEnabled || PublicEnabled;
    public string Summary { get; init; } = "No se pudo leer el firewall.";
    public string? Detail { get; init; }
}

public sealed class SecuritySnapshot
{
    public DefenderStatus Defender { get; init; } = new();
    public FirewallStatus Firewall { get; init; } = new();
    public IReadOnlyList<BAIOS.Core.Finding> Findings { get; init; } = [];
}
