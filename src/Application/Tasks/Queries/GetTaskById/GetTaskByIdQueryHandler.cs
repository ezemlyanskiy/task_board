using Application.Common.Interfaces.Persistence;
using Application.Tasks.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Tasks.Queries.GetTaskById;

public class GetTaskByIdQueryHandler(
ITasksRepository tasksRepository): IRequestHandler < GetTaskByIdQuery, ErrorOr < TaskResult >>
{
    private readonly ITasksRepository _tasksRepository = tasksRepository;

    public async Task < ErrorOr < TaskResult >> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
    {
        var task = await _tasksRepository.GetTaskById(query.Id);
        if (task is null) {
            return Errors.Task.DoesNotExist;
        }

        return new TaskResult(
            task.Id,
            task.Title,
            task.Description,
            task.Status.ToString(),
            task.Comment,
            task.UserId,
            task.SprintId,
            task.Files != null
                ? task.Files.Select(f => new TaskFileResult(f.Id, f.FileName)).ToList()
                : []);
    }
}
