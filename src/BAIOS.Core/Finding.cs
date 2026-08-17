namespace BAIOS.Core;

public sealed class Finding
{
    public required string Section { get; init; }
    public required Severity Severity { get; init; }
    public required string Title { get; init; }
    public string? Detail { get; init; }
    public required string Source { get; init; }
}
