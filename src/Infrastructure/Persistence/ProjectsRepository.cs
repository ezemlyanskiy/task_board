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

    public async Task<Project> CreateProject(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        return project;
    }

    public async Task<Project> UpdateProject(int id, string title, string description)
    {
        var project = await _context.Projects.FindAsync(id);
        
        if (project == null)
        {
            throw new Exception($"Project with id {id} not found.");
        }

        project.Title = title;
        project.Description = description;

        await _context.SaveChangesAsync();
        
        return project;
    }

    public async Task<int> DeleteProject(int id)
    {
        await _context.Projects.Where(p => p.Id == id).ExecuteDeleteAsync();

        return id;
    }
}
