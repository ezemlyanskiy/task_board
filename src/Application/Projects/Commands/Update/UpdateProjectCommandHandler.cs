using Application.Common.Interfaces.Persistance;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.Update;

public class UpdateProjectCommandHandler (IProjectsRepository projectsRepository) :
    IRequestHandler<UpdateProjectCommand, ErrorOr<Project>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<ErrorOr<Project>> Handle(
        UpdateProjectCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var project = await _projectsRepository.UpdateProject(
                command.Id,
                command.Title,
                command.Description);

            return project;
        }
        catch (Exception)
        {
            return Errors.Project.ProjectDoesNotExist;
        }
    }
}
