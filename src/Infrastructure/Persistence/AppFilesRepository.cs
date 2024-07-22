using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppFilesRepository(TaskBoardDbContext context) : IAppFilesRepository
{
    private readonly TaskBoardDbContext _context = context;

    public async Task<AppFile> CreateFile(AppFile file)
    {
        await _context.Files.AddAsync(file);
        await _context.SaveChangesAsync();

        return file;
    }

    public async Task DeleteFile(AppFile file)
    {
        await _context.Files.Where(f => f.Id == file.Id).ExecuteDeleteAsync();
    }
}
