using Application.Common.Interfaces.Persistence;

namespace Infrastructure.Persistence.Repositories;

public class ProjectsRepository(TaskBoardDbContext context) : IProjectsRepository
{
    private readonly TaskBoardDbContext _context = context;

    public async Task<List<Project>> GetAll()
    {
        return await _context.Projects.AsNoTracking().ToListAsync();
    }

    public async Task<Project> GetProjectById(ProjectId id)
    {
        var project = await _context.Projects
            .AsNoTracking()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        
        if (project is null)
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

    public async Task<Project> UpdateProject(
        ProjectId id,
        string title,
        string description)
    {
        var project = await _context.Projects.FindAsync(id);
        
        if (project is null)
        {
            throw new Exception($"Project with id {id} not found.");
        }

        var updatedProject = Project.Update(project, title, description);

        await _context.SaveChangesAsync();

        return updatedProject;
    }

    public async Task<ProjectId> DeleteProject(ProjectId id)
    {
        await _context.Projects.Where(p => p.Id == id).ExecuteDeleteAsync();

        return id;
    }
}
