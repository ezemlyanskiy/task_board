using Application.Common.Interfaces.Services;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.AddRole;

public class AddRoleCommandHandler (
    IIdentityService identityService)
    : IRequestHandler<AddRoleCommand, ErrorOr<bool>>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<ErrorOr<bool>> Handle(AddRoleCommand command, CancellationToken cancellationToken)
    {
        if (!await _identityService.DoesUserExistByEmail(command.Email))
        {
            return Errors.User.DoesNotExist;
        }

        await _identityService.AddToRoleAsync(command.Email, command.Role);

        return true;
    }
}
