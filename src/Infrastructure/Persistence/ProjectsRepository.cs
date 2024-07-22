using Infrastructure.Data;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ProjectsRepository(TaskBoardDbContext context) : IProjectsRepository
{
    private readonly TaskBoardDbContext _context = context;

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await _context.Projects
            .Include(p => p.Sprints)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Project?> GetProjectById(Guid id)
    {
        return await _context.Projects
            .Include(p => p.Sprints)
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Project> CreateProject(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        return project;
    }

    public async Task<Project> UpdateProject(Project project)
    {
        await _context.SaveChangesAsync();

        return project;
    }

    public async Task<Guid> DeleteProject(Guid id)
    {
        await _context.Projects.Where(p => p.Id == id).ExecuteDeleteAsync();

        return id;
    }
}
