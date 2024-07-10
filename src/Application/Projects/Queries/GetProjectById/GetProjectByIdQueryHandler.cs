namespace Application.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryHandler (
    IProjectsRepository projectsRepository) : IRequestHandler<GetProjectByIdQuery, ErrorOr<ProjectResult>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<ErrorOr<ProjectResult>> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var project = await _projectsRepository.GetProjectById(ProjectId.Create(query.Id));

            return new ProjectResult(project.Id.Value.ToString(), project.Title, project.Description);
        }
        catch (Exception)
        {
            return Errors.Project.ProjectDoesNotExist;
        }
    }
}