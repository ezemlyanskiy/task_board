using FluentValidation;

namespace Application.Authentication.Commands.BlockUser;

public class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
{
    public BlockUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
    }
}
