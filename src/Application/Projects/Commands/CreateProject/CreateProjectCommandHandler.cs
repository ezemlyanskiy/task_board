using Application.Common.Interfaces.Persistence;
using Application.Projects.Common;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler (IProjectsRepository projectsRepository)
    : IRequestHandler<CreateProjectCommand, ErrorOr<ProjectResult>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<ErrorOr<ProjectResult>> Handle(
        CreateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            UserIds = command.UserIds is not null ? command.UserIds : []
        };

        var createResult = await _projectsRepository.CreateProject(project);

        var projectResult = new ProjectResult(
            createResult.Id,
            createResult.Title,
            createResult.Description,
            createResult.UserIds!,
            []);
        
        return projectResult;
    }
}
