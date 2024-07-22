using Application.Common.Interfaces.Persistence;
using Application.Sprints.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Sprints.Queries.GetSprintById;

public class GetSprintByIdQueryHandler (ISprintsRepository sprintsRepository)
    : IRequestHandler<GetSprintByIdQuery, ErrorOr<SprintResult>>
{
    private readonly ISprintsRepository _sprintsRepository = sprintsRepository;

    public async Task<ErrorOr<SprintResult>> Handle(
        GetSprintByIdQuery query,
        CancellationToken cancellationToken)
    {
        var sprint = await _sprintsRepository.GetSprintById(query.Id);

        if (sprint is null)
        {
            return Errors.Sprint.DoesNotExist;
        }
        
        return new SprintResult(
            sprint.Id,
            sprint.Title!,
            sprint.Description!,
            sprint.StartDate,
            sprint.EndDate,
            sprint.Comment,
            sprint.ProjectId,
            sprint.UserIds,
            sprint.Tasks != null
                ? sprint.Tasks.Select(t => new SprintTaskResult(t.Id, t.Title, t.Description, t.Status.ToString(), t.UserId)).ToList()
                : [],
            sprint.Files != null
                ? sprint.Files.Select(f => new SprintFileResult(f.Id, f.FileName)).ToList()
                : []);
    }
}
