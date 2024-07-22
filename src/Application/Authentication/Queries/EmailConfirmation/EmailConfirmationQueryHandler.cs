using Application.Common.Interfaces.Services;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.EmailConfirmation;

public class EmailConfirmationQueryHandler(
    IIdentityService identityService) : IRequestHandler<EmailConfirmationQuery, ErrorOr<bool>>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<ErrorOr<bool>> Handle(EmailConfirmationQuery query, CancellationToken cancellationToken)
    {
        if (!await _identityService.DoesUserExistByEmail(query.Email))
        {
            return Errors.User.DoesNotExist;
        }

        var confirmResult = await _identityService.ConfirmEmailAsync(query.Email, query.Token);
        if (!confirmResult.Succeeded)
        {
            return Errors.EmailConfirmation.InvalidRequest;
        }

        return confirmResult.Succeeded;
    }
}
