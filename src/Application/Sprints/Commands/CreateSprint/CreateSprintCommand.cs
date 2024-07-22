using Application.Sprints.Common;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Sprints.Commands.CreateSprint;

public record CreateSprintCommand(
    string Title,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate,
    string? Comment,
    Guid ProjectId,
    List<Guid>? UserIds,
    IFormFileCollection? Files) : IRequest<ErrorOr<SprintResult>>;
