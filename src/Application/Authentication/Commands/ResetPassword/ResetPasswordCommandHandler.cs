using Application.Common.Interfaces.Services;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.ResetPassword;

public class ResetPasswordCommandHandler (
    IIdentityService identityService)
    : IRequestHandler<ResetPasswordCommand, ErrorOr<bool>>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<ErrorOr<bool>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        if (!await _identityService.DoesUserExistByEmail(command.Email))
        {
            return Errors.ResetPassword.InvalidRequest;
        }

        var result = await _identityService.ResetPasswordAsync(command.Email, command.Token, command.Password);
        if (!result.Succeeded)
        {
            return Errors.ResetPassword.InvalidRequest;
        }

        return result.Succeeded;
    }
}
