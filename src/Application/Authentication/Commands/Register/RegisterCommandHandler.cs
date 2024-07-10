using Domain.User.Enums;

namespace Application.Authentication.Commands.Register;

public class RegisterCommandHandler (
    IJwtTokenGenerator jwtTokenGenerator,
    IUsersRepository userRepository,
    IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUsersRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

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

        var hashedPassword = _passwordHasher.Generate(command.Password);

        var user = User.Create(
            firstName: command.FirstName,
            lastName: command.LastName,
            email: command.Email,
            password: hashedPassword,
            role: userRole);

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
