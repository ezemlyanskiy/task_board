using Application.Projects.Common;
using MediatR;

namespace Application.Projects.Queries.GetAll;

public record GetAllProjectsQuery : IRequest<IEnumerable<ProjectResult>>;
