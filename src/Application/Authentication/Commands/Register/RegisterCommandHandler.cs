using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Domain.Common.Errors;
using Domain.Entities;
using Domain.Enums;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Authentication.Commands.Register;

public class RegisterCommandHandler (
    IJwtTokenGenerator jwtTokenGenerator,
    IUsersRepository userRepository) : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUsersRepository _userRepository = userRepository;

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        if (!Enum.TryParse(command.Role, out Role userRole))
        {
            throw new Exception("There was an error during enum parsing.");
        }

        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Role = userRole,
            Password = command.Password
        };

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
