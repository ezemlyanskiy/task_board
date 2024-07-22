namespace Application.Tasks.Common;

public record TaskResult(
    Guid Id,
    string Title,
    string Description,
    string Status,
    string? Comment,
    Guid? UserId,
    Guid SprintId,
    List<TaskFileResult>? Files);

public record TaskFileResult(
    Guid Id,
    string FileName);
