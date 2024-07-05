using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.Delete;

public record DeleteProjectCommand(
    int Id) : IRequest<ErrorOr<int>>;
