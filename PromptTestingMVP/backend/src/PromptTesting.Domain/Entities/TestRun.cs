namespace PromptTesting.Domain.Entities;

public enum TestRunStatus { Pending, Running, Completed, Failed }

public class TestRun
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid PromptId { get; init; }
    public string ScopeTeam { get; init; } = string.Empty;
    public string ScopeBrokingSegment { get; init; } = string.Empty;
    public string ScopeGlobalLineOfBusiness { get; init; } = string.Empty;
    public string ScopeProduct { get; init; } = string.Empty;
    public TestRunStatus Status { get; private set; } = TestRunStatus.Pending;
    public decimal? Accuracy { get; private set; }
    public DateTime StartedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; private set; }
    public string? FailureReason { get; private set; }
    public string UserId { get; init; } = string.Empty;
    public string ContextSnapshot { get; init; } = string.Empty;

    public void MarkRunning() { if (Status != TestRunStatus.Pending) throw new InvalidOperationException(); Status = TestRunStatus.Running; }
    public void MarkCompleted(decimal accuracy)
    { if (Status != TestRunStatus.Running) throw new InvalidOperationException(); Accuracy = accuracy; CompletedAt = DateTime.UtcNow; Status = TestRunStatus.Completed; }
    public void MarkFailed(string reason)
    { if (Status is TestRunStatus.Completed or TestRunStatus.Failed) throw new InvalidOperationException(); FailureReason = reason; CompletedAt = DateTime.UtcNow; Status = TestRunStatus.Failed; }
}
