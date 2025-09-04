using Xunit;

namespace PromptTesting.IntegrationTests.Infrastructure;

public class TestDatabaseFixture : IAsyncLifetime
{
    public Task InitializeAsync()
    {
        // Placeholder for Testcontainers start
        return Task.CompletedTask;
    }

    public Task DisposeAsync() => Task.CompletedTask;
}

[CollectionDefinition("db")] public class DatabaseCollection : ICollectionFixture<TestDatabaseFixture> {}