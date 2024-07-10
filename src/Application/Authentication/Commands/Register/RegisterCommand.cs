namespace Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Role) : IRequest<ErrorOr<AuthenticationResult>>;
