using Domain.User.ValueObjects;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
        
        builder.Property(p => p.FirstName)
            .HasMaxLength(100);
        
        builder.Property(p => p.LastName)
            .HasMaxLength(100);
    }
}
