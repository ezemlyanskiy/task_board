using Application.Common.Interfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ProjectsRepository(ApplicationDbContext context) : IProjectsRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Project>> GetAll()
    {
        return await _context.Projects.AsNoTracking().ToListAsync();
    }

    public async Task<Project> GetProjectById(int id)
    {
        var project = await _context.Projects
            .AsNoTracking()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        
        if (project == null)
        {
            throw new Exception($"Project with {id} not found.");
        }

        return project;
    }

    public async Task<int> CreateProject(string title, string description)
    {
        var project = new Project(
            title,
            description);

        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        return project.Id;
    }

    public async Task<int> UpdateProject(int id, string title, string description)
    {
        var project = await _context.Projects
            .AsNoTracking()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        
        if (project == null)
        {
            throw new Exception($"Project with {id} not found.");
        }

        await _context.Projects
            .Where(p => p.Id == id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(p => p.Title, p => title)
                .SetProperty(p => p.Description, p => description));
        
        return id;
    }

    public async Task<int> DeleteProject(int id)
    {
        await _context.Projects.Where(p => p.Id == id).ExecuteDeleteAsync();

        return id;
    }
}
