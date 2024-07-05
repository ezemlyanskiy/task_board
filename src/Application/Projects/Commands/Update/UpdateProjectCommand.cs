using Application.Projects.Common;
using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.Update;

public record UpdateProjectCommand(
    int Id,
    string Title,
    string Description) : IRequest<ErrorOr<int>>;
