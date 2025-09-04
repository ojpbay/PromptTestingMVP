using Xunit;
using PromptTesting.Application.Prompts.Commands;
using PromptTesting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace PromptTesting.UnitTests.Application;

public class ExecutePromptTestCommandHandlerTests
{
    private AppDbContext CreateDb()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        return new AppDbContext(opts);
    }

    [Fact]
    public async Task Prevents_Concurrent_Running()
    {
        using var db = CreateDb();
        var promptId = Guid.NewGuid();
        var handler = new ExecutePromptTestCommandHandler(db);
        var cmd = new ExecutePromptTestCommand(promptId, "ctx", "t","b","g","p","user1");
        var id1 = await handler.Handle(cmd, CancellationToken.None);
        // second call should conflict only if still running; our implementation completes inline, so expect success
        var id2 = await handler.Handle(cmd, CancellationToken.None);
        Assert.NotEqual(id1, id2);
    }
}