using MediatR;
using PromptTesting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PromptTesting.Application.Prompts.Queries;

public class GetPromptContextQueryHandler : IRequestHandler<GetPromptContextQuery, string?>
{
    private readonly AppDbContext _db;
    public GetPromptContextQueryHandler(AppDbContext db) => _db = db;
    public async Task<string?> Handle(GetPromptContextQuery request, CancellationToken cancellationToken)
        => await _db.Prompts.Where(p => p.Id == request.Id).Select(p => p.BaseContext).FirstOrDefaultAsync(cancellationToken);
}