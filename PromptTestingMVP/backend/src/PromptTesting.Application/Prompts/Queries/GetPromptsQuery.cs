using MediatR;
using PromptTesting.Domain.Entities;

namespace PromptTesting.Application.Prompts.Queries;

public record GetPromptsQuery(string Team,string BrokingSegment,string GlobalLineOfBusiness,string Product) : IRequest<IReadOnlyList<Prompt>>;
