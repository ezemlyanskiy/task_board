using Application.Projects.Common;
using ErrorOr;
using MediatR;

namespace Application.Projects.Queries.GetProjectById;

public record GetProjectByIdQuery(Guid Id) : IRequest<ErrorOr<ProjectResult>>;
