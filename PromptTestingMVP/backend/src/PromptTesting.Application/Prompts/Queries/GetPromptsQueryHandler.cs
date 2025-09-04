using MediatR;
using PromptTesting.Domain.Entities;
using PromptTesting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PromptTesting.Application.Prompts.Queries;

public class GetPromptsQueryHandler : IRequestHandler<GetPromptsQuery, IReadOnlyList<Prompt>>
{
    private readonly AppDbContext _db;
    public GetPromptsQueryHandler(AppDbContext db) => _db = db;
    public async Task<IReadOnlyList<Prompt>> Handle(GetPromptsQuery request, CancellationToken cancellationToken)
    {
        return await _db.Prompts.AsNoTracking().ToListAsync(cancellationToken);
    }
}