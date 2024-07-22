using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.BlockUser;

public record BlockUserCommand(string Email) : IRequest<ErrorOr<bool>>;
