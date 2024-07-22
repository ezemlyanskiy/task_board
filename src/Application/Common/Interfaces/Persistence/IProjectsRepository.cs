using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IProjectsRepository
{
    public Task<IEnumerable<Project>> GetAllProjects();
    public Task<Project?> GetProjectById(Guid id);
    public Task<Project> CreateProject(Project project);
    public Task<Project> UpdateProject(Project project);
    public Task<Guid> DeleteProject(Guid id);
}
