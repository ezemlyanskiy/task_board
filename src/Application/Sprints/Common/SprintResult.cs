namespace Application.Sprints.Common;

public record SprintResult(
    Guid Id,
    string Title,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate,
    string? Comment,
    Guid ProjectId,
    List<Guid>? UsersIds,
    List<SprintTaskResult>? Tasks,
    List<SprintFileResult>? Files);

public record SprintTaskResult(
    Guid Id,
    string Title,
    string Description,
    string Status,
    Guid? UserId);

public record SprintFileResult(
    Guid Id,
    string FileName);
