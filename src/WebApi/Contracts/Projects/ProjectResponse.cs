namespace WebApi.Contracts.Projects;

public record ProjectResponse(
    Guid Id,
    string Title,
    string Description,
    List<Guid> UserIds,
    List<ProjectSprintResponse> Sprints);

public record ProjectSprintResponse(
    Guid Id,
    string Title,
    string Description);
