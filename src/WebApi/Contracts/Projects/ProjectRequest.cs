namespace WebApi.Contracts.Projects;

public record ProjectRequest(
    string Title,
    string Description,
    List<Guid>? UserIds);
