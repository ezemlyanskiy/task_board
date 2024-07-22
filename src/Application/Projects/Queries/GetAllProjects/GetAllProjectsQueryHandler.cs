using Application.Common.Interfaces.Persistence;
using Application.Projects.Common;
using MediatR;

namespace Application.Projects.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler (IProjectsRepository projectsRepository)
    : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectResult>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<IEnumerable<ProjectResult>> Handle(
        GetAllProjectsQuery query,
        CancellationToken cancellationToken)
    {
        var projects = await _projectsRepository.GetAllProjects();

        var projectsResult = projects.Select(p =>
            new ProjectResult(
                p.Id,
                p.Title,
                p.Description,
                p.UserIds!,
                p.Sprints != null 
                    ? p.Sprints!.Select(s => new ProjectSprintResult(s.Id, s.Title, s.Description)).ToList()
                    : []));

        return projectsResult;
    }
}
