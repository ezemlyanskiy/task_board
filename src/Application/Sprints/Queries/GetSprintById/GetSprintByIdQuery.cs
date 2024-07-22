using Application.Sprints.Common;
using ErrorOr;
using MediatR;

namespace Application.Sprints.Queries.GetSprintById;

public record GetSprintByIdQuery(Guid Id) : IRequest<ErrorOr<SprintResult>>;
