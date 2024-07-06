namespace WebApi.Contracts.Projects;

public record ProjectsRequest(
    int Id,
    string Title,
    string Description);
