using Infrastructure.Data;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class TasksRepository(TaskBoardDbContext context) : ITasksRepository
{
    private readonly TaskBoardDbContext _context = context;

    public async Task<IEnumerable<AppTask>> GetAllTasks()
    {
        return await _context.Tasks
            .Include(t => t.Files)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<AppTask?> GetTaskById(Guid id)
    {
        return await _context.Tasks
            .Include(t => t.Files)
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<AppTask> CreateTask(AppTask task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<AppTask> UpdateTask(AppTask task)
    {
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<Guid> DeleteTask(Guid id)
    {
        await _context.Tasks.Where(t => t.Id == id).ExecuteDeleteAsync();

        return id;
    }
}
