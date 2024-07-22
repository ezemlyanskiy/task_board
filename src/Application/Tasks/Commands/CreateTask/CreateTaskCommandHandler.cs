using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Application.Tasks.Common;
using Domain.Common.Errors;
using Domain.Entities;
using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Tasks.Commands.CreateTask;

public class CreateTaskCommandHandler (
    ITasksRepository tasksRepository,
    ISprintsRepository sprintsRepository,
    IAppFilesRepository filesRepository,
    IIdentityService identityService) : IRequestHandler<CreateTaskCommand, ErrorOr<TaskResult>>
{
    private readonly ITasksRepository _tasksRepository = tasksRepository;
    private readonly ISprintsRepository _sprintsRepository = sprintsRepository;
    private readonly IAppFilesRepository _filesRepository = filesRepository;
    private readonly IIdentityService _identityService = identityService;

    public async Task<ErrorOr<TaskResult>> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        if (await _sprintsRepository.GetSprintById(command.SprintId) is null)
        {
            return Errors.Sprint.DoesNotExist;
        }

        if (command.UserId is not null && await _identityService.DoesUserExistById(command.UserId.ToString()!) is true)
        {
            return Errors.User.DoesNotExist;
        }

        var files = new List<AppFile>();
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

                files.Add(result);
            }
        }

        var task = new AppTask
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            Status = (Status)Enum.Parse(typeof(Status), command.Status!),
            Comment = command.Comment,
            SprintId = command.SprintId,
            UserId = command.UserId,
            Files = files
        };

        var createResult = await _tasksRepository.CreateTask(task);

        var taskResult = new TaskResult(
            createResult.Id,
            createResult.Title,
            createResult.Description,
            createResult.Status.ToString(),
            createResult.Comment,
            createResult.UserId,
            createResult.SprintId,
            createResult.Files is not null
                ? createResult.Files.Select(f => new TaskFileResult(f.Id, f.FileName)).ToList()
                : []
        );

        return taskResult;
    }
}
