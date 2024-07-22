using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Application.Tasks.Common;
using Domain.Common.Errors;
using Domain.Entities;
using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Tasks.Commands.UpdateTask;

public class UpdateTaskCommandHandler (
    ITasksRepository tasksRepository,
    ISprintsRepository sprintsRepository,
    IAppFilesRepository filesRepository,
    IIdentityService identityService
) :
    IRequestHandler<UpdateTaskCommand, ErrorOr<TaskResult>>
{
    private readonly ITasksRepository _tasksRepository = tasksRepository;
    private readonly ISprintsRepository _sprintsRepository = sprintsRepository;
    private readonly IAppFilesRepository _filesRepository = filesRepository;
    private readonly IIdentityService _identityService = identityService;


    public async Task<ErrorOr<TaskResult>> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await _tasksRepository.GetTaskById(command.Id);
        if (task is null)
        {
            return Errors.Task.DoesNotExist;
        }

        task.Title = command.Title;
        task.Description = command.Description;
        task.Status = (Status)Enum.Parse(typeof(Status), command.Status);;
        task.Comment = command.Comment;

        if (command.UserId is not null)
        {
            if (await _identityService.DoesUserExistById(command.UserId.ToString()!) is false)
            {
                return Errors.User.DoesNotExist;
            }

            task.UserId = command.UserId;
        }

        var sprint = await _sprintsRepository.GetSprintById(command.SprintId);
        if (sprint is null)
        {
            return Errors.Sprint.DoesNotExist;
        }
        task.Sprint = sprint;
        task.SprintId = sprint.Id;

        if (task.Files is not null)
        {
            foreach (var file in task.Files)
            {
                await _filesRepository.DeleteFile(file);
            }
        }

        if (command.Files is not null)
        {
            foreach (var file in command.Files)
            {
                using var memoryStream = new MemoryStream();

                file.CopyTo(memoryStream);

                var newFile = new AppFile
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName,
                    Data = memoryStream.ToArray(),
                };

                var result = await _filesRepository.CreateFile(newFile);

                task.Files!.Add(result);
            }
        }

        var updatedTask = await _tasksRepository.UpdateTask(task);

        if (command.Files is null)
        {
            updatedTask.Files!.Clear();
        }

        var taskResult = new TaskResult(
            updatedTask.Id,
            updatedTask.Title,
            updatedTask.Description,
            updatedTask.Status.ToString(),
            updatedTask.Comment,
            updatedTask.UserId,
            updatedTask.SprintId,
            updatedTask.Files != null
                ? updatedTask.Files.Select(t => new TaskFileResult(t.Id, t.FileName)).ToList()
                : []
        );

        return taskResult;
    }
}
