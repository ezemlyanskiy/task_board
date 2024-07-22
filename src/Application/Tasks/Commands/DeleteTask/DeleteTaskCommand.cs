using ErrorOr;
using MediatR;

namespace Application.Tasks.Commands.DeleteTask;

public record DeleteTaskCommand(Guid Id) : IRequest<ErrorOr<Guid>>;
