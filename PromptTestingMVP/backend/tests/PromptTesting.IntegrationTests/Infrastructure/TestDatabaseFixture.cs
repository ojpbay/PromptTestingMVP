using Xunit;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Data.SqlClient;
using PromptTesting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PromptTesting.IntegrationTests.Infrastructure;

public class TestDatabaseFixture : IAsyncLifetime
{
    private readonly TestcontainersContainer _sqlContainer;
    public string ConnectionString { get; private set; } = string.Empty;

    public TestDatabaseFixture()
    {
        _sqlContainer = new ContainerBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithEnvironment("ACCEPT_EULA","Y")
            .WithEnvironment("MSSQL_SA_PASSWORD","Your_strong_password123")
            .WithPortBinding(14333,true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();
        var hostPort = _sqlContainer.GetMappedPublicPort(14333);
        ConnectionString = $"Server=localhost,{hostPort};User=sa;Password=Your_strong_password123;TrustServerCertificate=True;";

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