using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.Create;

public record CreateProjectCommand(
    string Title,
    string Description) : IRequest<ErrorOr<Project>>;
