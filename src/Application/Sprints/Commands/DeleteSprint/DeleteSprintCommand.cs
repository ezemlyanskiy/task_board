using ErrorOr;
using MediatR;

namespace Application.Sprints.Commands.DeleteSprint;

public record DeleteSprintCommand(Guid Id) : IRequest<ErrorOr<Guid>>;
