using Application.Common.Interfaces.Persistance;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.Create;

public class CreateProjectCommandHandler (IProjectsRepository projectsRepository) :
    IRequestHandler<CreateProjectCommand, ErrorOr<Project>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<ErrorOr<Project>> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = new Project(command.Title, command.Description);

        return await _projectsRepository.CreateProject(project);
    }
}