using Xunit;
using Testcontainers.MsSql;
using Microsoft.Data.SqlClient;
using PromptTesting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PromptTesting.IntegrationTests.Infrastructure;

public class TestDatabaseFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _sqlContainer;
    public string ConnectionString { get; private set; } = string.Empty;

    public TestDatabaseFixture()
    {
        _sqlContainer = new MsSqlBuilder()
            .WithPassword("Your_strong_password123")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();
        ConnectionString = _sqlContainer.GetConnectionString();

        // Create database and apply schema via EnsureCreated
        var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(ConnectionString + "Database=PromptTestingTest;").Options;
        using var db = new AppDbContext(options);
        await db.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _sqlContainer.DisposeAsync();
    }
}

[CollectionDefinition("db")] public class DatabaseCollection : ICollectionFixture<TestDatabaseFixture> {}