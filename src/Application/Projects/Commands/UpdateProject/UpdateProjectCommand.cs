namespace Application.Projects.Commands.UpdateProject;

public record UpdateProjectCommand(
    Guid Id,
    string Title,
    string Description) : IRequest<ErrorOr<Project>>;
