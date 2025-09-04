using FluentValidation;
using PromptTesting.Application.Prompts.Commands;

namespace PromptTesting.Application.Validation;

public class ExecutePromptTestCommandValidator : AbstractValidator<ExecutePromptTestCommand>
{
    public ExecutePromptTestCommandValidator()
    {
        RuleFor(x=>x.Context).NotEmpty();
        RuleFor(x=>x.Team).NotEmpty();
        RuleFor(x=>x.BrokingSegment).NotEmpty();
        RuleFor(x=>x.GlobalLineOfBusiness).NotEmpty();
        RuleFor(x=>x.Product).NotEmpty();
    }
}
