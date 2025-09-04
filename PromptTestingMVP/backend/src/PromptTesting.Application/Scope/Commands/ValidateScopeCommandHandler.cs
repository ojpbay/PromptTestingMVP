using MediatR;
using PromptTesting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PromptTesting.Application.Scope.Commands;

public class ValidateScopeCommandHandler : IRequestHandler<ValidateScopeCommand, bool>
{
    private readonly AppDbContext _db;
    public ValidateScopeCommandHandler(AppDbContext db) => _db = db;
    public async Task<bool> Handle(ValidateScopeCommand request, CancellationToken cancellationToken)
    {
        return await _db.ScopeValidationRules.AnyAsync(r =>
            r.Team == request.Team &&
            r.BrokingSegment == request.BrokingSegment &&
            r.GlobalLineOfBusiness == request.GlobalLineOfBusiness &&
            r.Product == request.Product &&
            r.IsValid, cancellationToken);
    }
}