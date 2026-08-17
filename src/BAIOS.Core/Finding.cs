namespace BAIOS.Core;

public sealed class Finding
{
    public required string Section { get; init; }
    public required Severity Severity { get; init; }
    public required string Title { get; init; }
    public string? Detail { get; init; }
    public required string Source { get; init; }

    public static Finding Create(
        string section,
        Severity severity,
        string title,
        string source,
        string? detail = null) =>
        new()
        {
            Section = section,
            Severity = severity,
            Title = title,
            Source = source,
            Detail = detail
        };
}
