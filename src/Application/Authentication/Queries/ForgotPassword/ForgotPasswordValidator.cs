using FluentValidation;

namespace Application.Authentication.Queries.ForgotPassword;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordQuery>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.ClientUri).NotEmpty();
    }
}
