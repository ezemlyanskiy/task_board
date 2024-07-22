using Application.Tasks.Common;
using ErrorOr;
using MediatR;

namespace Application.Tasks.Queries.GetTaskById;

public record GetTaskByIdQuery(Guid Id) : IRequest<ErrorOr<TaskResult>>;
