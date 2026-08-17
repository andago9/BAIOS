using BAIOS.Core;

namespace BAIOS.Diagnostics;

public sealed class CpuInfo
{
    public string Name { get; init; } = "CPU desconocida";
    public int? Cores { get; init; }
    public int? LoadPercent { get; init; }
}

public sealed class MemoryInfo
{
    public long TotalBytes { get; init; }
    public long FreeBytes { get; init; }
    public int UsedPercent => TotalBytes <= 0 ? 0 : (int)Math.Round((TotalBytes - FreeBytes) * 100.0 / TotalBytes);
}

public sealed class VolumeInfo
{
    public string Name { get; init; } = "";
    public string Label { get; init; } = "";
    public long TotalBytes { get; init; }
    public long FreeBytes { get; init; }
    public int FreePercent => TotalBytes <= 0 ? 0 : (int)Math.Round(FreeBytes * 100.0 / TotalBytes);
}

public sealed class SmartDisk
{
    public string Instance { get; init; } = "";
    public bool PredictFailure { get; init; }
}

public sealed class ServiceRow
{
    public string Name { get; init; } = "";
    public string DisplayName { get; init; } = "";
    public string Status { get; init; } = "";
    public string StartType { get; init; } = "";
}

public sealed class ProcessRow
{
    public string Name { get; init; } = "";
    public int Id { get; init; }
    public long WorkingSetBytes { get; init; }
}

public sealed class StartupRow
{
    public string Name { get; init; } = "";
    public string Command { get; init; } = "";
    public string Location { get; init; } = "";
}

public sealed class DriverRow
{
    public string Name { get; init; } = "";
    public int ErrorCode { get; init; }
}

public sealed class WindowsIdentityInfo
{
    public string Product { get; init; } = "";
    public string DisplayVersion { get; init; } = "";
    public string Build { get; init; } = "";
    public string Summary => string.Join(" ", new[] { Product, DisplayVersion, string.IsNullOrEmpty(Build) ? null : $"build {Build}" }.Where(s => !string.IsNullOrWhiteSpace(s)));
}

public sealed class WindowsUpdateStatus
{
    public int? PendingCount { get; init; }
    public string? LastHotfix { get; init; }
    public string Summary { get; init; } = "No se pudo leer Windows Update.";
    public Severity Severity { get; init; } = Severity.Warning;
}

public sealed class DiagnosticsSnapshot
{
    public CpuInfo Cpu { get; init; } = new();
    public MemoryInfo Memory { get; init; } = new();
    public IReadOnlyList<VolumeInfo> Volumes { get; init; } = [];
    public IReadOnlyList<SmartDisk> Smart { get; init; } = [];
    public bool SmartAvailable { get; init; }
    public IReadOnlyList<ServiceRow> AutoStoppedServices { get; init; } = [];
    public int RunningServices { get; init; }
    public int TotalServices { get; init; }
    public IReadOnlyList<ProcessRow> TopProcesses { get; init; } = [];
    public int ProcessCount { get; init; }
    public IReadOnlyList<StartupRow> StartupItems { get; init; } = [];
    public IReadOnlyList<DriverRow> ProblemDrivers { get; init; } = [];
    public WindowsIdentityInfo Windows { get; init; } = new();
    public WindowsUpdateStatus WindowsUpdate { get; init; } = new();
    public IReadOnlyList<Finding> Findings { get; init; } = [];
}
