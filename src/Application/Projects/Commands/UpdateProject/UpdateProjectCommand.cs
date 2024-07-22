using Application.Projects.Common;
using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.UpdateProject;

public record UpdateProjectCommand(
    Guid Id,
    string Title,
    string Description,
    List<Guid> UserIds) : IRequest<ErrorOr<ProjectResult>>;
