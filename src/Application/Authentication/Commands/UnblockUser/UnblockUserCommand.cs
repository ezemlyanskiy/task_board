using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.UnblockUser;

public record UnblockUserCommand(string Email) : IRequest<ErrorOr<bool>>;
