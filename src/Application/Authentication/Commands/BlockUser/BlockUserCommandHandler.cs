using Application.Common.Interfaces.Services;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.BlockUser;

public class BlockUserCommandHandler (
    IIdentityService identityService)
    : IRequestHandler<BlockUserCommand, ErrorOr<bool>>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<ErrorOr<bool>> Handle(BlockUserCommand command, CancellationToken cancellationToken)
    {
        if (!await _identityService.DoesUserExistByEmail(command.Email))
        {
            return Errors.User.DoesNotExist;
        }

        var blockResult = await _identityService.SetLockoutEndDateAsync(command.Email, true);
        if (!blockResult.Succeeded)
        {
            return Errors.SetLockedOut.InvalidRequest;
        }

        return blockResult.Succeeded;
    }
}
