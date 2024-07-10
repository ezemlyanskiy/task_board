namespace Application.Projects.Queries.GetProjectById;

public record GetProjectByIdQuery(Guid Id) : IRequest<ErrorOr<ProjectResult>>;
