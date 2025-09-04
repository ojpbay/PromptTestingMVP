namespace PromptTesting.Domain.Entities;

public enum PromptStatus { Active, Draft, Archived }

public class Prompt
{
    public Guid Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string Version { get; private set; } = "1";
    public string DataPoint { get; private set; } = string.Empty;
    public PromptStatus Status { get; private set; } = PromptStatus.Active;
    public string BaseContext { get; private set; } = string.Empty;
    public decimal? LastAccuracy { get; private set; }
    public DateTime? LastRunAt { get; private set; }

    public void UpdateLastResult(decimal accuracy, DateTime when)
    {
        if (accuracy < 0 || accuracy > 100) throw new ArgumentOutOfRangeException(nameof(accuracy));
        LastAccuracy = accuracy;
        LastRunAt = when;
    }
}
