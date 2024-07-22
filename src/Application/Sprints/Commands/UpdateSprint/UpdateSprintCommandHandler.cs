using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Application.Sprints.Common;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Sprints.Commands.UpdateSprint;

public class UpdateSprintCommandHandler (
    ISprintsRepository sprintsRepository,
    IProjectsRepository projectsRepository,
    IAppFilesRepository filesRepository,
    IIdentityService identityService) : IRequestHandler<UpdateSprintCommand, ErrorOr<SprintResult>>
{
    private readonly ISprintsRepository _sprintsRepository = sprintsRepository;
    private readonly IProjectsRepository _projectsRepository = projectsRepository;
    private readonly IAppFilesRepository _filesRepository = filesRepository;
    private readonly IIdentityService _identityService = identityService;

    public async Task<ErrorOr<SprintResult>> Handle(UpdateSprintCommand command, CancellationToken cancellationToken)
    {
        var sprint = await _sprintsRepository.GetSprintById(command.Id);
        if (sprint is null)
        {
            return Errors.Sprint.DoesNotExist;
        }

        var project = await _projectsRepository.GetProjectById(command.ProjectId);
        if (project is null)
        {
            return Errors.Project.DoesNotExist;
        }

        if (command.UserIds is not null)
        {
            foreach (var userId in command.UserIds)
            {
                if (!await _identityService.DoesUserExistById(userId.ToString()))
                {
                    return Errors.User.DoesNotExist;
                }
            }

            sprint.UserIds = command.UserIds.ToList();
        }

        if (sprint.Files is not null)
        {
            foreach (var file in sprint.Files)
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

                sprint.Files!.Add(result);
            }
        }

        sprint.Title = command.Title;
        sprint.Description = command.Description;
        sprint.StartDate = command.StartDate;
        sprint.EndDate = command.EndDate;
        sprint.Comment = command.Comment;
        sprint.ProjectId = command.ProjectId;

        var updatedSprint = await _sprintsRepository.UpdateSprint(sprint);

        if (command.Files is null)
        {
            updatedSprint.Files!.Clear();
        }

        var sprintResult = new SprintResult(
            updatedSprint.Id,
            updatedSprint.Title,
            updatedSprint.Description,
            updatedSprint.StartDate,
            updatedSprint.EndDate,
            updatedSprint.Comment,
            updatedSprint.ProjectId,
            updatedSprint.UserIds,
            updatedSprint.Tasks is not null
                ? updatedSprint.Tasks.Select(t => new SprintTaskResult(t.Id, t.Title, t.Description, t.Status.ToString(), t.UserId)).ToList()
                : [],
            updatedSprint.Files is not null
                ? updatedSprint.Files.Select(f => new SprintFileResult(f.Id, f.FileName)).ToList()
                : []
        );

        return sprintResult;
    }
}
