using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface ISprintsRepository
{
    public Task<IEnumerable<Sprint>> GetAllSprints();
    public Task<Sprint?> GetSprintById(Guid id);
    public Task<Sprint> CreateSprint(Sprint sprint);
    public Task<Sprint> UpdateSprint(Sprint sprint);
    public Task<Guid> DeleteSprint(Guid id);
}
