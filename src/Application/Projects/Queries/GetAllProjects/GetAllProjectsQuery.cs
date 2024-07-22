using Application.Projects.Common;
using MediatR;

namespace Application.Projects.Queries.GetAllProjects;

public record GetAllProjectsQuery : IRequest<IEnumerable<ProjectResult>>;
