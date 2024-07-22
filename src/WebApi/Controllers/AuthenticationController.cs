using Application.Authentication.Commands.AddRole;
using Application.Authentication.Commands.BlockUser;
using Application.Authentication.Commands.Register;
using Application.Authentication.Commands.ResetPassword;
using Application.Authentication.Commands.UnblockUser;
using Application.Authentication.Common;
using Application.Authentication.Queries.EmailConfirmation;
using Application.Authentication.Queries.ForgotPassword;
using Application.Authentication.Queries.Login;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Accounts;

namespace WebApi.Controllers;

[Route("api/v1/accounts")]
[AllowAnonymous]
public class AccountsController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(_mapper.Map<RegisterResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        ErrorOr<LoginResult> loginResult = await _mediator.Send(query);

        return loginResult.Match(
            loginResult => Ok(_mapper.Map<LoginResponse>(loginResult)),
            errors => Problem(errors));
    }

    [HttpGet("emailconfirmation")]
    public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
    {
        var query = _mapper.Map<EmailConfirmationQuery>((email, token));

        ErrorOr<bool> emailConfirmationResult = await _mediator.Send(query);

        return emailConfirmationResult.Match(
            emailConfirmationResult => Ok(),
            errors => Problem(errors));
    }

    [HttpPost("forgotpassword")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var query = _mapper.Map<ForgotPasswordQuery>(request);

        var forgotPasswordResult = await _mediator.Send(query);

        return forgotPasswordResult.Match(
            forgotPasswordResult => Ok(),
            errors => Problem(errors));
    }

    [HttpPost("resetpassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var query = _mapper.Map<ResetPasswordCommand>(request);

        var resetPasswordResult = await _mediator.Send(query);

        return resetPasswordResult.Match(
            resetPasswordResult => Ok(),
            errors => Problem(errors));
    }

    [HttpPost("blockuser")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BlockUser(BlockUserRequest request)
    {
        var command = _mapper.Map<BlockUserCommand>(request);

        var blockResult = await _mediator.Send(command);

        return blockResult.Match(
            blockResult => Ok(),
            errors => Problem(errors));
    }

    [HttpPost("unblockuser")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UnblockUser(UnblockUserRequest request)
    {
        var command = _mapper.Map<UnblockUserCommand>(request);

        var unblockResult = await _mediator.Send(command);

        return unblockResult.Match(
            blockResult => Ok(),
            errors => Problem(errors));
    }

    [HttpPost("addrole")]
    public async Task<IActionResult> AddRole(AddRoleRequest request)
    {
        var command = _mapper.Map<AddRoleCommand>(request);

        var addRoleResult = await _mediator.Send(command);

        return addRoleResult.Match(
            addRoleResult => Ok(),
            errors => Problem(errors));
    }
}
