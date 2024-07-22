using WebApi.Contracts.Common;

namespace WebApi.Contracts.Sprints;

public record SprintResponse(
    Guid Id,
    string Title,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate,
    string Comment,
    Guid ProjectId,
    IEnumerable<Guid>? UserIds,
    IEnumerable<SprintTaskResponse> Tasks,
    IEnumerable<FileResponse> Files);

public record SprintTaskResponse(
    Guid Id,
    string Title,
    string Description,
    string Status,
    Guid? UserId);
