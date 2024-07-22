using Application.Common.Interfaces.Persistence;
using Application.Projects.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryHandler(IProjectsRepository projectsRepository)
    : IRequestHandler<GetProjectByIdQuery, ErrorOr<ProjectResult>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<ErrorOr<ProjectResult>> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken)
    {
        var project = await _projectsRepository.GetProjectById(query.Id);
        if (project is null) {
            return Errors.Project.DoesNotExist;
        }

        return new ProjectResult(
            project.Id,
            project.Title,
            project.Description,
            project.UserIds!,
            project.Sprints != null
                ? project.Sprints.Select(s => new ProjectSprintResult(s.Id, s.Title, s.Description)).ToList()
                : []);
    }
}
