using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class TaskBoardUserConfiguration : IEntityTypeConfiguration<TaskBoardUser>
{
    public void Configure(EntityTypeBuilder<TaskBoardUser> builder)
    {
        builder.HasData(
            new TaskBoardUser
            {
                Id = "471c0978-e868-44b9-a45d-f3a40198c1ea",
                Email = "admin@gmail.com",
                EmailConfirmed = true,                
                UserName = "admin@gmail.com",
            },
            new TaskBoardUser
            {
                Id = "97be6e79-2183-4b31-91e4-f5c2b4a4df7c",
                Email = "manager@gmail.com",
                EmailConfirmed = true,                
                UserName = "manager@gmail.com"
            },
            new TaskBoardUser
            {
                Id = "5d37d156-e561-488e-afa5-5f05d21a3e0b",
                Email = "user@gmail.com",
                EmailConfirmed = true,                
                UserName = "user@gmail.com"
            }
        );
    }
}
