using FluentValidation;

namespace Application.Sprints.Queries.GetSprintById;

public class GetSprintByIdQueryValidator : AbstractValidator<GetSprintByIdQuery>
{
    public GetSprintByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
