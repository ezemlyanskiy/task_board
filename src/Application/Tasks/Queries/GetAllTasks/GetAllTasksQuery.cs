using Application.Tasks.Common;
using MediatR;

namespace Application.Tasks.Queries.GetAllTasks;

public record GetAllTasksQuery : IRequest<IEnumerable<TaskResult>>;
