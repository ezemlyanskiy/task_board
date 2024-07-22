using Application.Projects.Queries.GetAllProjects;
using Application.Projects.Queries.GetProjectById;
using MediatR;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Projects;
using Application.Projects.Commands.CreateProject;
using Application.Projects.Commands.UpdateProject;
using Application.Projects.Commands.DeleteProject;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[Route("api/v1/projects")]
public class ProjectsController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        Console.Write(HttpContext.User);
        
        var projectsResult = await _mediator.Send(new GetAllProjectsQuery());

        return Ok(projectsResult.Select(_mapper.Map<ProjectResponse>));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetProjectById(Guid id)
    {
        var projectResult = await _mediator.Send(new GetProjectByIdQuery(id));

        return projectResult.Match(
            projectResult => Ok(_mapper.Map<ProjectResponse>(projectResult)),
            errors => Problem(errors));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProject(ProjectRequest request)
    {
        var command = _mapper.Map<CreateProjectCommand>(request);

        var projectResult = await _mediator.Send(command);

        return projectResult.Match(
            projectResult => Ok(_mapper.Map<ProjectResponse>(projectResult)),
            errors => Problem(errors));
    }

    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProject(
        Guid id,
        ProjectRequest request)
    {
        var command = _mapper.Map<UpdateProjectCommand>((id, request));

        var projectResult = await _mediator.Send(command);

        return projectResult.Match(
            projectResult => Ok(_mapper.Map<ProjectResponse>(projectResult)),
            errors => Problem(errors));
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteProjectCommand(id)); 
    
        return deleteResult.Match(
            deleteResult => Ok(deleteResult),
            errors => Problem(errors));
    }
}
