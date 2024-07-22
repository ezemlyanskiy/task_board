using Application.Common.Interfaces.Persistence;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Tasks.Commands.DeleteTask;

public class DeleteTaskCommandHandler (
    ITasksRepository tasksRepository,
    IAppFilesRepository filesRepository) : IRequestHandler<DeleteTaskCommand, ErrorOr<Guid>>
{
    private readonly ITasksRepository _tasksRepository = tasksRepository;
    private readonly IAppFilesRepository _filesRepository = filesRepository;

    public async Task<ErrorOr<Guid>> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await _tasksRepository.GetTaskById(command.Id);
        if (task is null)
        {
            return Errors.Task.DoesNotExist;
        }

        return await _tasksRepository.DeleteTask(command.Id);
    }
}
