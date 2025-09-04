using Xunit;
using PromptTesting.Domain.Entities;

namespace PromptTesting.UnitTests.Domain;

public class PromptTests
{
    [Fact]
    public void UpdateLastResult_Should_Set_Values()
    {
        var p = new Prompt { Id = Guid.NewGuid(), };
        p.UpdateLastResult(50, DateTime.UtcNow);
        Assert.Equal(50, p.LastAccuracy);
    }
}
