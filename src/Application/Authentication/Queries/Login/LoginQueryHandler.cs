using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.Services.Email;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.Login;

public class LoginQueryHandler (
    IEmailSender emailSender,
    IJwtTokenGenerator jwtTokenGenerator,
    IIdentityService identityService) : IRequestHandler<LoginQuery, ErrorOr<LoginResult>>
{
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IIdentityService _identityService = identityService;

    public async Task<ErrorOr<LoginResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if (!await _identityService.DoesUserExistByEmail(query.Email))
        {
            return Errors.User.DoesNotExist;
        }

        if (await _identityService.IsLockedOutAsync(query.Email))
        {
            return Errors.User.LockedOut;
        }

        if (!await _identityService.IsEmailConfirmedAsync(query.Email))
        {
            return Errors.User.MailIsNotConfirm;
        }

        if (!await _identityService.CheckPasswordAsync(query.Email, query.Password))
        {
            await _identityService.AccessFailedAsync(query.Email);

            if (await _identityService.IsLockedOutAsync(query.Email))
            {
                var content = $"Your account is locked out. If you want to reset the password, " +
                    $"you can use the Forgot Password link on the login page.";

                var message = new Message([query.Email!], "Locked out account information", content, null!);

                await _emailSender.SendEmailAsync(message);

                return Errors.User.LockedOut;
            }

            return Errors.Authentication.InvalidCredentials;
        }

        var roles = await _identityService.GetRolesAsync(query.Email);
        var userDto = await _identityService.GetUserData(query.Email);
        var token = _jwtTokenGenerator.GenerateToken(userDto, roles!);

        await _identityService.ResetAccessFailedCountAsync(query.Email);

        return new LoginResult { Token = token };
    }
}
