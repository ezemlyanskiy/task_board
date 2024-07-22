using Application.Tasks.Common;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Tasks.Commands.UpdateTask;

public record UpdateTaskCommand(
    Guid Id,
    string Title,
    string Description,
    string Status,
    string? Comment,
    Guid SprintId,
    Guid? UserId,
    IFormFileCollection? Files) : IRequest<ErrorOr<TaskResult>>;
