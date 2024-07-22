using FluentValidation;

namespace Application.Sprints.Commands.DeleteSprint;

public class DeleteSprintCommandValidator : AbstractValidator<DeleteSprintCommand>
{
    public DeleteSprintCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
