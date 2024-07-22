using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.ResetPassword;

public record ResetPasswordCommand(
    string Password,
    string ConfirmPassword,
    string Email,
    string Token) : IRequest<ErrorOr<bool>>;
