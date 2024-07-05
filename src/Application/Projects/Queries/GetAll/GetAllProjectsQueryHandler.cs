using Application.Common.Interfaces.Persistance;
using Application.Projects.Common;
using MediatR;

namespace Application.Projects.Queries.GetAll;

public class GetAllProjectsQueryHandler (
    IProjectsRepository projectrsRepository) : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectResult>>
{
    private readonly IProjectsRepository _projectrsRepository = projectrsRepository;

    public async Task<IEnumerable<ProjectResult>> Handle(GetAllProjectsQuery query, CancellationToken cancellationToken)
    {
        var projects = await _projectrsRepository.GetAll();

        var result = projects.Select(x => new ProjectResult(x));

        return result;
    }
}
