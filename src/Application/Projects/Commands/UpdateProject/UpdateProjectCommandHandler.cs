namespace Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler (IProjectsRepository projectsRepository) :
    IRequestHandler<UpdateProjectCommand, ErrorOr<Project>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<ErrorOr<Project>> Handle(
        UpdateProjectCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var updatedProject = await _projectsRepository.UpdateProject(
                ProjectId.Create(command.Id),
                command.Title,
                command.Description);

            return updatedProject;
        }
        catch (Exception)
        {
            return Errors.Project.ProjectDoesNotExist;
        }
    }
}
