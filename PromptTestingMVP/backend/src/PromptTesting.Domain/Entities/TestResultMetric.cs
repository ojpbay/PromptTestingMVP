namespace PromptTesting.Domain.Entities;

public class TestResultMetric
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid TestRunId { get; init; }
    public string MetricName { get; init; } = string.Empty;
    public string MetricValue { get; init; } = string.Empty;
}
