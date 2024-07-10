namespace Infrastructure.Persistence;

public class TaskBoardDbContext(DbContextOptions<TaskBoardDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(TaskBoardDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
