namespace Application.Projects.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler (IProjectsRepository projectsRepository)
    : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectResult>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<IEnumerable<ProjectResult>> Handle(
        GetAllProjectsQuery query,
        CancellationToken cancellationToken)
    {
        var projects = await _projectsRepository.GetAll();

        var result = projects.Select(
            p => new ProjectResult(p.Id.Value.ToString(), p.Title, p.Description));

        return result;
    }
}
