using MediatR;

namespace PromptTesting.Application.Prompts.Queries;

public record GetPromptContextQuery(Guid Id) : IRequest<string?>;
