using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(Guid Id) : IRequest<ErrorOr<Guid>>;
