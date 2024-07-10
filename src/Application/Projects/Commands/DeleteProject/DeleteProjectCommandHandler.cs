namespace Application.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler (IProjectsRepository projectsRepository) :
    IRequestHandler<DeleteProjectCommand, ErrorOr<ProjectId>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<ErrorOr<ProjectId>> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var projectId = await _projectsRepository.DeleteProject(ProjectId.Create(command.Id));

            return projectId;
        }
        catch (Exception)
        {
            return Errors.Project.ProjectDoesNotExist;
        }

    }
}
