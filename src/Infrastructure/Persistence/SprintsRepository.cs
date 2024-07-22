using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class SprintsRepository(TaskBoardDbContext context) : ISprintsRepository
{
    private readonly TaskBoardDbContext _context = context;

    public async Task<IEnumerable<Sprint>> GetAllSprints()
    {

        return await _context.Sprints
            .Include(s => s.Files)
            .Include(s => s.Tasks)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Sprint?> GetSprintById(Guid id)
    {
        return await _context.Sprints
            .Include(s => s.Files)
            .Include(s => s.Tasks)
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Sprint> CreateSprint(Sprint sprint)
    {
        await _context.Sprints.AddAsync(sprint);
        await _context.SaveChangesAsync();

        return sprint;
    }

    public async Task<Sprint> UpdateSprint(Sprint sprint)
    {
        _context.Entry(sprint).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return sprint;
    }

    public async Task<Guid> DeleteSprint(Guid id)
    {
        await _context.Sprints.Where(s => s.Id == id).ExecuteDeleteAsync();

        return id;
    }
}
