using Application.Tasks.Commands.CreateTask;
using Application.Tasks.Commands.DeleteTask;
using Application.Tasks.Commands.UpdateTask;
using Application.Tasks.Queries.GetAllTasks;
using Application.Tasks.Queries.GetTaskById;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Tasks;

namespace WebApi.Controllers;

[Route("api/v1/tasks")]
public class TasksController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasksResult = await _mediator.Send(new GetAllTasksQuery());

        return Ok(tasksResult.Select(_mapper.Map<TaskResponse>));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var taskResult = await _mediator.Send(new GetTaskByIdQuery(id));

        return taskResult.Match(
            taskResult => Ok(_mapper.Map<TaskResponse>(taskResult)),
            errors => Problem(errors));
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> CreateTask(TaskRequest request)
    {
        var command = new CreateTaskCommand(
            request.Title,
            request.Description,
            request.Status,
            request.Comment,
            request.SprintId,
            request.UserId,
            request.Files!
        );

        var taskResult = await _mediator.Send(command);

        return taskResult.Match(
            taskResult => Ok(_mapper.Map<TaskResponse>(taskResult)),
            errors => Problem(errors));
    }

    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> UpdateTask(Guid id, TaskRequest request)
    {
        var command = new UpdateTaskCommand(
            id,
            request.Title,
            request.Description,
            request.Status,
            request.Comment,
            request.SprintId,
            request.UserId,
            request.Files!
        );

        var taskResult = await _mediator.Send(command);

        return taskResult.Match(
            taskResult => Ok(_mapper.Map<TaskResponse>(taskResult)),
            errors => Problem(errors));
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteTaskCommand(id));

        return deleteResult.Match(
            deleteResult => Ok(deleteResult),
            errors => Problem(errors));
    }
}
