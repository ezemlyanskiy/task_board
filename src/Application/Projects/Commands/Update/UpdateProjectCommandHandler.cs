using Application.Common.Interfaces.Persistance;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.Update;

public class UpdateProjectCommandHandler (IProjectsRepository projectsRepository) :
    IRequestHandler<UpdateProjectCommand, ErrorOr<int>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<ErrorOr<int>> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var projectId = await _projectsRepository.UpdateProject(command.Id, command.Title, command.Description);

            return projectId;
        }
        catch (Exception)
        {
            return Errors.Project.ProjectDoesNotExist;
        }
    }
}
