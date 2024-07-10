using Application.Projects.Queries.GetAllProjects;
using Application.Projects.Queries.GetProjectById;
using Application.Projects.Commands.DeleteProject;

namespace WebApi.Controllers;

[Route("api/v1/[controller]")]
public class ProjectsController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = _mapper.Map<GetAllProjectsQuery>(new GetAllProjectsQuery());

        var projects = await _mediator.Send(query);

        return Ok(projects);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetProjectById(Guid id)
    {
        var getResult = await _mediator.Send(new GetProjectByIdQuery(id));

        return getResult.Match(
            project => Ok(project),
            errors => Problem(errors));
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> CreateProject(ProjectRequest request)
    {
        var command = _mapper.Map<CreateProjectCommand>(request);

        var createProjectResult = await _mediator.Send(command);

        return createProjectResult.Match(
            project => Ok(_mapper.Map<ProjectResponse>(project)),
            errors => Problem(errors));
    }

    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> UpdateProject(
        Guid id,
        ProjectRequest request)
    {
        var command = _mapper.Map<UpdateProjectCommand>((id, request));

        var updateProjectResult = await _mediator.Send(command);

        return updateProjectResult.Match(
            project => Ok(_mapper.Map<ProjectResponse>(project)),
            errors => Problem(errors));
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteProjectCommand(id));
    
        return deleteResult.Match(
            id => Ok(id),
            errors => Problem(errors));
    }
}
