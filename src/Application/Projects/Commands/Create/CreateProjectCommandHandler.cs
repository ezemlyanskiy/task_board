using Application.Common.Interfaces.Persistance;
using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.Create;

public class CreateProjectCommandHandler (IProjectsRepository projectsRepository) :
    IRequestHandler<CreateProjectCommand, ErrorOr<int>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<ErrorOr<int>> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var projectId = await _projectsRepository.CreateProject(command.Title, command.Description);

        return projectId;
    }
}
