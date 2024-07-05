using Application.Common.Interfaces.Persistance;
using Application.Projects.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Projects.Queries.GetById;

public class GetProjectByIdQueryHandler (
    IProjectsRepository projectrsRepository) : IRequestHandler<GetProjectByIdQuery, ErrorOr<ProjectResult>>
{
    private readonly IProjectsRepository _projectrsRepository = projectrsRepository;

    public async Task<ErrorOr<ProjectResult>> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var project = await _projectrsRepository.GetProjectById(query.Id);

            return new ProjectResult(project);
        }
        catch (Exception)
        {
            return Errors.Project.ProjectDoesNotExist;
        }
    }
}
