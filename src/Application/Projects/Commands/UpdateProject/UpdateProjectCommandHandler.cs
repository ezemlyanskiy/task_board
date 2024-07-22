using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Application.Projects.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler (
    IProjectsRepository projectsRepository,
    IIdentityService identityService) : IRequestHandler<UpdateProjectCommand, ErrorOr<ProjectResult>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;
    private readonly IIdentityService _identityService = identityService;

    public async Task<ErrorOr<ProjectResult>> Handle(
        UpdateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var project = await _projectsRepository.GetProjectById(command.Id);
        if (project is null)
        {
            return Errors.Project.DoesNotExist;
        }

        if (command.UserIds != null)
        {
            foreach (var userId in command.UserIds)
            {
                if (!await _identityService.DoesUserExistById(userId.ToString()))
                {
                    return Errors.User.DoesNotExist;
                }
            }
        }

        project.Title = command.Title;
        project.Description = command.Description;

        await _projectsRepository.UpdateProject(project);

        var projectResult = new ProjectResult(
            project.Id,
            project.Title,
            project.Description,
            project.UserIds!,
            project.Sprints != null
                ? project.Sprints.Select(s => new ProjectSprintResult(s.Id, s.Title, s.Description)).ToList()
                : []
        );

        return projectResult;
    }
}
