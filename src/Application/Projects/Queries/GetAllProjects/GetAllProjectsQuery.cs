namespace Application.Projects.Queries.GetAllProjects;

public record GetAllProjectsQuery : IRequest<IEnumerable<ProjectResult>>;
