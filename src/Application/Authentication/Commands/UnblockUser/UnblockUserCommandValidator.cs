using FluentValidation;

namespace Application.Authentication.Commands.UnblockUser;

public class UnblockUserCommandValidator : AbstractValidator<UnblockUserCommand>
{
    public UnblockUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
    }
}
