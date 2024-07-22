namespace Application.Projects.Common;

public record ProjectResult(
    Guid Id,
    string Title,
    string Description,
    List<Guid> Users,
    List<ProjectSprintResult> Sprints);

public record ProjectSprintResult(
    Guid Id,
    string Title,
    string Description);
