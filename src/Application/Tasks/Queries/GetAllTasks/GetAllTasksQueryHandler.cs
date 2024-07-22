using Application.Common.Interfaces.Persistence;
using Application.Tasks.Common;
using MediatR;

namespace Application.Tasks.Queries.GetAllTasks;

public class GetAllTasksQueryHandler (ITasksRepository tasksRepository)
    : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskResult>>
{
    private readonly ITasksRepository _tasksRepository = tasksRepository;

    public async Task<IEnumerable<TaskResult>> Handle(
        GetAllTasksQuery query,
        CancellationToken cancellationToken)
    {
        var tasks = await _tasksRepository.GetAllTasks();

        var TasksResult = tasks.Select(t =>
            new TaskResult(
                t.Id,
                t.Title,
                t.Description,
                t.Status.ToString(),
                t.Comment,
                t.UserId,
                t.SprintId,
                t.Files != null
                    ? t.Files!.Select(f => new TaskFileResult(f.Id, f.FileName)).ToList()
                    : []));

        return TasksResult;
    }
}
