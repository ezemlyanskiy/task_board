using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                UserId = "471c0978-e868-44b9-a45d-f3a40198c1ea",
                RoleId = "3e16bc5c-f133-432f-8eeb-fb8b38d15d02"
            },
            new IdentityUserRole<string>
            {
                UserId = "97be6e79-2183-4b31-91e4-f5c2b4a4df7c",
                RoleId = "d70f10a2-c592-451c-b6e5-0efa9969ae2f"
            },
            new IdentityUserRole<string>
            {
                UserId = "5d37d156-e561-488e-afa5-5f05d21a3e0b",
                RoleId = "5b6003b6-7c90-498b-ad6c-5ed1790aa494"
            }
        );
    }
}
