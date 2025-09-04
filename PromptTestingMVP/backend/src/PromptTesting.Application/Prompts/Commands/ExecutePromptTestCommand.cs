using MediatR;

namespace PromptTesting.Application.Prompts.Commands;

public record ExecutePromptTestCommand(Guid PromptId,string Context,string Team,string BrokingSegment,string GlobalLineOfBusiness,string Product,string UserId) : IRequest<Guid>;
