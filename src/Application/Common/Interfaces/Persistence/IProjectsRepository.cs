using Domain.Entities;

namespace Application.Common.Interfaces.Persistance;

public interface IProjectsRepository
{
    public Task<List<Project>> GetAll();
    public Task<Project> GetProjectById(int id);
    public Task<Project> CreateProject(Project project);
    public Task<Project> UpdateProject(int id, string title, string description);
    public Task<int> DeleteProject(int id);
}
