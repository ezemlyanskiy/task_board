namespace WebApi.Contracts.Projects;

public record CreateUpdateProjectRequest(
    string Title,
    string Description);
