using Application.Common.Interfaces.Persistence;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler (
    IProjectsRepository projectsRepository,
    ISprintsRepository sprintsRepository) : IRequestHandler<DeleteProjectCommand, ErrorOr<Guid>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;
    private readonly ISprintsRepository _sprintsRepository = sprintsRepository;

    public async Task<ErrorOr<Guid>> Handle(
        DeleteProjectCommand command,
        CancellationToken cancellationToken)
    {
        var project = await _projectsRepository.GetProjectById(command.Id);
        if (project is null)
        {
            return Errors.Project.DoesNotExist;
        }

        if (project.Sprints != null)
        {
            foreach (var sprint in project.Sprints)
            {
                await _sprintsRepository.DeleteSprint(sprint.Id);
            }
        }

        return await _projectsRepository.DeleteProject(project.Id);
    }
}
