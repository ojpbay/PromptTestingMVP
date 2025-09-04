using Microsoft.EntityFrameworkCore;
using PromptTesting.Domain.Entities;

namespace PromptTesting.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedAsync(this AppDbContext db, CancellationToken ct = default)
    {
        if (!await db.Prompts.AnyAsync(ct))
        {
            db.Prompts.Add(new Prompt
            {
                Id = Guid.NewGuid()
            });
            await db.SaveChangesAsync(ct);
        }

        if (!await db.ScopeValidationRules.AnyAsync(ct))
        {
            db.ScopeValidationRules.Add(new ScopeValidationRule
            {
                Team = "TeamA",
                BrokingSegment = "SegmentA",
                GlobalLineOfBusiness = "GLoB1",
                Product = "Prod1",
                IsValid = true
            });
            await db.SaveChangesAsync(ct);
        }
    }
}