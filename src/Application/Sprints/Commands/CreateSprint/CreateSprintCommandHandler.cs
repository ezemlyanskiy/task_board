using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Application.Sprints.Common;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Sprints.Commands.CreateSprint;

public class CreateSprintCommandHandler (
    ISprintsRepository sprintsRepository,
    IProjectsRepository projectsRepository,
    IAppFilesRepository filesRepository,
    IIdentityService identityService) : IRequestHandler<CreateSprintCommand, ErrorOr<SprintResult>>
{
    private readonly ISprintsRepository _sprintsRepository = sprintsRepository;
    private readonly IProjectsRepository _projectsRepository = projectsRepository;
    private readonly IAppFilesRepository _filesRepository = filesRepository;
    private readonly IIdentityService _identityService = identityService;

    public async Task<ErrorOr<SprintResult>> Handle(CreateSprintCommand command, CancellationToken cancellationToken)
    {
        var sprint = new Sprint
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            Comment = command.Comment,
            ProjectId = command.ProjectId,
            UserIds = [],
            Tasks = [],
            Files = []
        };

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

                sprint.UserIds.Add(userId);
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

                sprint.Files.Add(result);
            }
        }

        var createResult = await _sprintsRepository.CreateSprint(sprint);

        var sprintResult = new SprintResult(
            createResult.Id,
            createResult.Title,
            createResult.Description,
            createResult.StartDate,
            createResult.EndDate,
            createResult.Comment,
            createResult.ProjectId,
            createResult.UserIds,
            createResult.Tasks != null
                ? createResult.Tasks.Select(t => new SprintTaskResult(t.Id, t.Title, t.Description, t.Status.ToString(), t.UserId)).ToList()
                : [],
            createResult.Files != null
                ? createResult.Files.Select(f => new SprintFileResult(f.Id, f.FileName)).ToList()
                : []);

        return sprintResult;
    }
}
