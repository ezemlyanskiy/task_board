using Application.Common.Interfaces.Persistence;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Sprints.Commands.DeleteSprint;

public class DeleteSprintCommandHandler (ISprintsRepository sprintsRepository)
    : IRequestHandler<DeleteSprintCommand, ErrorOr<Guid>>
{
    private readonly ISprintsRepository _sprintsRepository = sprintsRepository;

    public async Task<ErrorOr<Guid>> Handle(DeleteSprintCommand command, CancellationToken cancellationToken)
    {
        var sprint = await _sprintsRepository.GetSprintById(command.Id);
        if (sprint is null)
        {
            return Errors.Sprint.DoesNotExist;
        }

        return await _sprintsRepository.DeleteSprint(sprint.Id);
    }
}
