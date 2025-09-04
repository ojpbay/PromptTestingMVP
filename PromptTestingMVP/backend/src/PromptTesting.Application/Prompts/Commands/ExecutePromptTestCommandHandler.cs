using MediatR;
using PromptTesting.Infrastructure.Persistence;
using PromptTesting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PromptTesting.Application.Prompts.Commands;

public class ExecutePromptTestCommandHandler : IRequestHandler<ExecutePromptTestCommand, Guid>
{
    private readonly AppDbContext _db;
    public ExecutePromptTestCommandHandler(AppDbContext db) => _db = db;
    public async Task<Guid> Handle(ExecutePromptTestCommand request, CancellationToken cancellationToken)
    {
        // Concurrency guard (simplified): prevent running test for same prompt + user
        var running = await _db.TestRuns.AnyAsync(r => r.PromptId == request.PromptId && r.UserId == request.UserId && r.Status == TestRunStatus.Running, cancellationToken);
        if (running) throw new InvalidOperationException("Test already running");
        var tr = new TestRun
        {
            PromptId = request.PromptId,
            ScopeTeam = request.Team,
            ScopeBrokingSegment = request.BrokingSegment,
            ScopeGlobalLineOfBusiness = request.GlobalLineOfBusiness,
            ScopeProduct = request.Product,
            UserId = request.UserId,
            ContextSnapshot = request.Context
        };
        _db.TestRuns.Add(tr);
        await _db.SaveChangesAsync(cancellationToken);
        // Simulate async processing completion (for MVP inline). In real system offload to background worker.
        // Compute fake accuracy & update Prompt (T030)
        var accuracy = Random.Shared.Next(60, 100);
        tr.MarkRunning();
        tr.MarkCompleted(accuracy);
        var prompt = await _db.Prompts.FirstOrDefaultAsync(p => p.Id == tr.PromptId, cancellationToken);
        if (prompt != null && tr.Accuracy.HasValue)
        {
            prompt.UpdateLastResult(tr.Accuracy.Value, tr.CompletedAt ?? DateTime.UtcNow);
        }
        await _db.SaveChangesAsync(cancellationToken);
        return tr.Id;
    }
}