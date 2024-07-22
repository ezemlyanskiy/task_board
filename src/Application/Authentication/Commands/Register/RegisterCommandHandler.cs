using Application.Authentication.Common;
using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.Services.Email;
using Domain.Common.Constants;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;

namespace Application.Authentication.Commands.Register;

public class RegisterCommandHandler (
    IIdentityService identityService,
    IEmailSender emailSender)
    : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IIdentityService _identityService = identityService;
    private readonly IEmailSender _emailSender = emailSender;

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (await _identityService.DoesUserExistByEmail(command.Email))
        {
            return Errors.User.DuplicateEmail;
        }

        var (createResult, userId) = await _identityService.Add(command.Email, command.Password);

        if (!createResult.Succeeded)
        {
            return Errors.User.InvalidRequest;
        }

        var token = await _identityService.GenerateEmailConfirmationTokenAsync(command.Email);

        var param = new Dictionary<string, string?>
        {
            { "token", token },
            { "email", command.Email }
        };

        var callback = QueryHelpers.AddQueryString(command.ClientUri!, param);
        
        var message = new Message([ command.Email! ], "Email Confirmation Token", callback, null!);

        await _emailSender.SendEmailAsync(message);

        await _identityService.AddToRoleAsync(command.Email, Roles.User);

        return new AuthenticationResult { UserId = userId };
    }
}
