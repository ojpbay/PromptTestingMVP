using Xunit;
using PromptTesting.Domain.Entities;

namespace PromptTesting.UnitTests.Domain;

public class TestRunTests
{
    [Fact]
    public void MarkCompleted_Sets_Status_And_Accuracy()
    {
        var tr = new TestRun { PromptId = Guid.NewGuid(), UserId = "u", ContextSnapshot = "c" };
        tr.MarkRunning();
        tr.MarkCompleted(75);
        Assert.Equal(TestRunStatus.Completed, tr.Status);
        Assert.Equal(75, tr.Accuracy);
        Assert.NotNull(tr.CompletedAt);
    }

    [Fact]
    public void MarkFailed_Sets_Status_And_Reason()
    {
        var tr = new TestRun { PromptId = Guid.NewGuid(), UserId = "u", ContextSnapshot = "c" };
        tr.MarkRunning();
        tr.MarkFailed("boom");
        Assert.Equal(TestRunStatus.Failed, tr.Status);
        Assert.Equal("boom", tr.FailureReason);
    }
}