using Domain.Enums;

namespace WebApi.Contracts.Authentication;

public record AuthenticationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    Role Role,
    string Token);
