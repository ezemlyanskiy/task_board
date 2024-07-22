using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IAppFilesRepository
{
    Task<AppFile> CreateFile(AppFile file);
    Task DeleteFile(AppFile file);
}
