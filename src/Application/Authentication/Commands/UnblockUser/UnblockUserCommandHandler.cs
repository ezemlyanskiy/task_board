using Application.Common.Interfaces.Services;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.UnblockUser;

public class UnblockUserCommandHandler (
    IIdentityService identityService)
    : IRequestHandler<UnblockUserCommand, ErrorOr<bool>>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<ErrorOr<bool>> Handle(UnblockUserCommand command, CancellationToken cancellationToken)
    {
        if (!await _identityService.DoesUserExistByEmail(command.Email))
        {
            return Errors.User.DoesNotExist;
        }

        var unblockResult = await _identityService.SetLockoutEndDateAsync(command.Email, false);
        if (!unblockResult.Succeeded)
        {
            return Errors.SetLockedOut.InvalidRequest;
        }

        return unblockResult.Succeeded;
    }
}
