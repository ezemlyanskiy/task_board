namespace Application.Projects.Commands.CreateProject;

public record CreateProjectCommand(
    string Title,
    string Description) : IRequest<ErrorOr<Project>>;
