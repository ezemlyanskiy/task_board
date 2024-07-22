using Application.Projects.Common;
using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.CreateProject;

public record CreateProjectCommand(
    string Title,
    string Description,
    List<Guid>UserIds) : IRequest<ErrorOr<ProjectResult>>;
