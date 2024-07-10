namespace Application.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler (IProjectsRepository projectsRepository) :
    IRequestHandler<CreateProjectCommand, ErrorOr<Project>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<ErrorOr<Project>> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = Project.Create(command.Title, command.Description);

        return await _projectsRepository.CreateProject(project);
    }
}
