using Application.Projects.Common;
using ErrorOr;
using MediatR;

namespace Application.Projects.Queries.GetById;

public record GetProjectByIdQuery(int Id) : IRequest<ErrorOr<ProjectResult>>;
