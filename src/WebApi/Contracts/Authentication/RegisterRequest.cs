using Domain.Enums;

namespace WebApi.Contracts.Authentication;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    Role Role,
    string Password);

