using Application.Sprints.Common;
using MediatR;

namespace Application.Sprints.Queries.GetAllSprints;

public record GetAllSpintsQuery : IRequest<IEnumerable<SprintResult>>;
