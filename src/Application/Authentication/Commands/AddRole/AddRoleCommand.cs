using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.AddRole;

public record AddRoleCommand(
    string Email,
    string Role) : IRequest<ErrorOr<bool>>;
