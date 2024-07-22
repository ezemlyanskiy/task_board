using Application.Common.Interfaces.Persistence;
using Application.Sprints.Common;
using MediatR;

namespace Application.Sprints.Queries.GetAllSprints;

public class GetAllSprintsQueryHandler (ISprintsRepository sprintsRepository)
    : IRequestHandler<GetAllSpintsQuery, IEnumerable<SprintResult>>
{
    private readonly ISprintsRepository _sprintsRepository = sprintsRepository;

    public async Task<IEnumerable<SprintResult>> Handle(
        GetAllSpintsQuery query,
        CancellationToken cancellationToken)
    {
        var sprints = await _sprintsRepository.GetAllSprints();

        var result = sprints.Select(s => new SprintResult(
            s.Id,
            s.Title!,
            s.Description!,
            s.StartDate,
            s.EndDate,
            s.Comment,
            s.ProjectId,
            s.UserIds,
            s.Tasks != null
                ? s.Tasks.Select(t => new SprintTaskResult(t.Id, t.Title, t.Description, t.Status.ToString(), t.UserId)).ToList()
                : [],
            s.Files != null
                ? s.Files.Select(f => new SprintFileResult(f.Id, f.FileName)).ToList()
                : []));
        
        return result!;
    }
}
