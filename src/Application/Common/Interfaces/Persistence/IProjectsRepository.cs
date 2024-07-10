namespace Application.Common.Interfaces.Persistence;

public interface IProjectsRepository
{
    public Task<List<Project>> GetAll();
    public Task<Project> GetProjectById(ProjectId id);
    public Task<Project> CreateProject(Project project);
    public Task<Project> UpdateProject(ProjectId id, string title, string description);
    public Task<ProjectId> DeleteProject(ProjectId id);
}
