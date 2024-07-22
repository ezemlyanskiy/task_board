using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface ITaskBoardDbContext
{
    DbSet<Project> Projects { get; set; }
    DbSet<Sprint> Sprints { get; set; }
}
