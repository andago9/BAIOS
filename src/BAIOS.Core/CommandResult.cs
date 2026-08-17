namespace BAIOS.Core;

public sealed class CommandResult
{
    public int? ExitCode { get; init; }
    public string StandardOutput { get; init; } = "";
    public string StandardError { get; init; } = "";
    public bool Elevated { get; init; }
    public bool Success { get; init; }
    public bool Cancelled { get; init; }
    public string? Message { get; init; }

    public string CombinedOutput
    {
        get
        {
            if (string.IsNullOrWhiteSpace(StandardError))
            {
                return StandardOutput;
            }

            if (string.IsNullOrWhiteSpace(StandardOutput))
            {
                return StandardError;
            }

            return StandardOutput + Environment.NewLine + StandardError;
        }
    }
}
