using MediatR;

namespace PromptTesting.Application.Scope.Commands;

public class ValidateScopeCommandHandler : IRequestHandler<ValidateScopeCommand, bool>
{
    public Task<bool> Handle(ValidateScopeCommand request, CancellationToken cancellationToken)
    {
        // MVP: all combinations valid; future: check rules table
        return Task.FromResult(true);
    }
}