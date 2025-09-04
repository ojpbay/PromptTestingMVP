using MediatR;

namespace PromptTesting.Application.Scope.Commands;

public record ValidateScopeCommand(string Team,string BrokingSegment,string GlobalLineOfBusiness,string Product) : IRequest<bool>;
