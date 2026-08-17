namespace BAIOS.Core;

public sealed class Session
{
    public AppMode Mode { get; set; } = AppMode.Home;
    public string Hostname { get; } = Environment.MachineName;
    public DateTimeOffset StartedAt { get; } = DateTimeOffset.Now;
    public string EngineVersion { get; init; } = "4.0.0-dev";
    public List<Finding> Findings { get; } = [];
    public List<string> Recommendations { get; } = [];

    public void Add(Finding finding) => Findings.Add(finding);

    public void AddRange(IEnumerable<Finding> findings) => Findings.AddRange(findings);

    public void AddRecommendation(string text)
    {
        if (!string.IsNullOrWhiteSpace(text) && !Recommendations.Contains(text))
        {
            Recommendations.Add(text);
        }
    }
}
