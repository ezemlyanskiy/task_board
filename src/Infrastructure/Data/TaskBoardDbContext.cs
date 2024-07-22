using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data.Configurations;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class TaskBoardDbContext(DbContextOptions<TaskBoardDbContext> options)
    : IdentityDbContext<TaskBoardUser, IdentityRole, string>(options), ITaskBoardDbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<Sprint> Sprints { get; set; }
    public DbSet<AppTask> Tasks { get; set; }
    public DbSet<AppFile> Files { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Project>()
            .HasMany(p => p.Sprints)
            .WithOne(s => s.Project)
            .HasForeignKey(s => s.ProjectId);
        
        builder.Entity<Sprint>()
            .HasOne(s => s.Project)
            .WithMany(p => p.Sprints);
        
        builder.Entity<Sprint>()
            .HasMany(s => s.Files)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<AppTask>()
            .HasOne(t => t.Sprint)
            .WithMany(s => s.Tasks);
        
        builder.Entity<AppTask>()
            .HasMany(t => t.Files)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.ApplyConfiguration(new IdentityRoleConfiguration());
        builder.ApplyConfiguration(new TaskBoardUserConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());

        base.OnModelCreating(builder);
    }
}
