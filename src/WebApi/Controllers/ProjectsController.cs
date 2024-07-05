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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var query = new GetProjectByIdQuery(id);

        var getResult = await _mediator.Send(query);

        return getResult.Match(
            getResult => Ok(getResult),
            errors => Problem(errors));
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> CreateProject([FromBody] ProjectsRequest request)
    {
        var command = _mapper.Map<CreateProjectCommand>(request);

        var createResult = await _mediator.Send(command);

        return createResult.Match(
            createResult => Ok(createResult),
            errors => Problem(errors));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectsRequest request)
    {
        var projectsRequest = new ProjectsRequest(id, request.Title, request.Description);

        var command = _mapper.Map<UpdateProjectCommand>(projectsRequest);

        Console.WriteLine(command);

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            updateResult => Ok(updateResult),
            errors => Problem(errors));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var command = new DeleteProjectCommand(id);

        var deleteResult = await _mediator.Send(command);
    
        return deleteResult.Match(
            deleteResult => Ok(deleteResult),
            errors => Problem(errors));
    }
}
