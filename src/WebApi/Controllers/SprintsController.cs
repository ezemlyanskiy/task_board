using MediatR;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Application.Sprints.Queries.GetAllSprints;
using Microsoft.AspNetCore.Authorization;
using Application.Sprints.Queries.GetSprintById;
using WebApi.Contracts.Sprints;
using Application.Sprints.Commands.CreateSprint;
using Application.Sprints.Commands.UpdateSprint;
using Application.Sprints.Commands.DeleteSprint;
using Mapster;

namespace WebApi.Controllers;

[Route("api/v1/sprints")]
public class SprintsController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetAllSprints()
    {
        var sprintsResult = await _mediator.Send(new GetAllSpintsQuery());

        return Ok(sprintsResult.Select(_mapper.Map<SprintResponse>));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetSprintById(Guid id)
    {
        var sprintResult = await _mediator.Send(new GetSprintByIdQuery(id));

        return sprintResult.Match(
            sprintResult => Ok(_mapper.Map<SprintResponse>(sprintResult)),
            errors => Problem(errors));
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> CreateSprint(SprintRequest request)
    {
        var command = new CreateSprintCommand(
            request.Title,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.Comment,
            request.ProjectId,
            request.UserIds,
            request.Files
        );

        var sprintResult = await _mediator.Send(command);

        return sprintResult.Match(
            sprintResult => Ok(_mapper.Map<SprintResponse>(sprintResult)),
            errors => Problem(errors));
    }

    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> UpdateSprint(Guid id, SprintRequest request)
    {
        var command = new UpdateSprintCommand(
            id,
            request.Title,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.Comment,
            request.ProjectId,
            request.UserIds,
            request.Files
        );

        var sprintResult = await _mediator.Send(command);

        return sprintResult.Match(
            sprintResult => Ok(_mapper.Map<SprintResponse>(sprintResult)),
            errors => Problem(errors));
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> DeleteSprint(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteSprintCommand(id)); 
    
        return deleteResult.Match(
            deleteResult => Ok(deleteResult),
            errors => Problem(errors));
    }
}
