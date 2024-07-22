using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.Services.Email;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;

namespace Application.Authentication.Queries.ForgotPassword;

public class ForgotPasswordQueryHandler(
    IIdentityService identityService,
    IEmailSender emailSender) : IRequestHandler<ForgotPasswordQuery, ErrorOr<bool>>
{
    private readonly IIdentityService _identityService = identityService;
    private readonly IEmailSender _emailSender = emailSender;

    public async Task<ErrorOr<bool>> Handle(ForgotPasswordQuery query, CancellationToken cancellationToken)
    {
        if (!await _identityService.DoesUserExistByEmail(query.Email))
        {
            return Errors.User.DoesNotExist;
        }

        var token = await _identityService.GeneratePasswordResetTokenAsync(query.Email);

        var param = new Dictionary<string, string>
        {
            { "token", token },
            { "email", query.Email! }
        };

        var callback = QueryHelpers.AddQueryString(query.ClientUri!, param!);

        var message = new Message([query.Email!], "Reset Password", callback, null!);

        await _emailSender.SendEmailAsync(message);

        return true;
    }
}
