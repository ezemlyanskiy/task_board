namespace WebApi.Contracts.Projects;

public record UpdateProjectsRequest(
    string Title,
    string Description);
