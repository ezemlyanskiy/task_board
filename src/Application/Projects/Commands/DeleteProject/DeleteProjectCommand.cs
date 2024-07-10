namespace Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(Guid Id) : IRequest<ErrorOr<ProjectId>>;
