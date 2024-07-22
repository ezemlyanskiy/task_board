using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface ITasksRepository
{
    public Task<IEnumerable<AppTask>> GetAllTasks();
    public Task<AppTask?> GetTaskById(Guid id);
    public Task<AppTask> CreateTask(AppTask task);
    public Task<AppTask> UpdateTask(AppTask task);
    public Task<Guid> DeleteTask(Guid id);
}
