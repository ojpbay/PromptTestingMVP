using Microsoft.EntityFrameworkCore;
using PromptTesting.Domain.Entities;

namespace PromptTesting.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Prompt> Prompts => Set<Prompt>();
    public DbSet<TestRun> TestRuns => Set<TestRun>();
    public DbSet<TestResultMetric> TestResultMetrics => Set<TestResultMetric>();
    public DbSet<ScopeValidationRule> ScopeValidationRules => Set<ScopeValidationRule>();
}

public class ScopeValidationRule
{
    public int Id { get; set; }
    public string Team { get; set; } = string.Empty;
    public string BrokingSegment { get; set; } = string.Empty;
    public string GlobalLineOfBusiness { get; set; } = string.Empty;
    public string Product { get; set; } = string.Empty;
    public bool IsValid { get; set; } = true;
}
