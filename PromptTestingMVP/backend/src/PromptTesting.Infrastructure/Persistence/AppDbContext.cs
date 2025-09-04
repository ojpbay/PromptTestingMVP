using Microsoft.EntityFrameworkCore;
using PromptTesting.Domain.Entities;

namespace PromptTesting.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Prompt> Prompts => Set<Prompt>();
    public DbSet<TestRun> TestRuns => Set<TestRun>();
    public DbSet<TestResultMetric> TestResultMetrics => Set<TestResultMetric>();
}
