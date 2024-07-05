using Application.Common.Interfaces.Persistance;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Projects.Commands.Delete;

public class DeleteProjectCommandHandler (IProjectsRepository projectsRepository) :
    IRequestHandler<DeleteProjectCommand, ErrorOr<int>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<ErrorOr<int>> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var projectId = await _projectsRepository.DeleteProject(command.Id);

            return projectId;
        }
        catch (Exception)
        {
            return Errors.Project.ProjectDoesNotExist;
        }

    }
}
