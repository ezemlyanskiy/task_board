using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Projects;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using MapsterMapper;
using Application.Projects.Queries.GetAll;
using Application.Projects.Queries.GetById;
using Application.Projects.Commands.Create;
using Application.Projects.Commands.Update;
using Application.Projects.Commands.Delete;

namespace WebApi.Controllers;

[Route("api/v1/[controller]")]
public class ProjectsController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public ProjectsController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = _mapper.Map<GetAllProjectsQuery>(new GetAllProjectsQuery());

        var projects = await _mediator.Send(query);

        return Ok(projects);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var getResult = await _mediator.Send(new GetProjectByIdQuery(id));

        return getResult.Match(
            project => Ok(project),
            errors => Problem(errors));
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> CreateProject(
        [FromBody] CreateUpdateProjectRequest request)
    {
        var command = _mapper.Map<CreateProjectCommand>(request);

        var createProjectResult = await _mediator.Send(command);

        return createProjectResult.Match(
            project => Ok(_mapper.Map<ProjectReponse>(project)),
            errors => Problem(errors));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> UpdateProject(
        [FromBody] CreateUpdateProjectRequest request,
        int id)
    {
        var command = _mapper.Map<UpdateProjectCommand>((request, id));

        var updateProjectResult = await _mediator.Send(command);

        return updateProjectResult.Match(
            project => Ok(_mapper.Map<ProjectReponse>(project)),
            errors => Problem(errors));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var command = new DeleteProjectCommand(id);

        var deleteResult = await _mediator.Send(command);
    
        return deleteResult.Match(
            id => Ok(id),
            errors => Problem(errors));
    }
}