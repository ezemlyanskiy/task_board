using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.Create;

public record CreateProjectCommand(
    int Id,
    string Title,
    string Description) : IRequest<ErrorOr<int>>;
