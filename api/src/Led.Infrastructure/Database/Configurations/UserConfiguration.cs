using Led.Domain.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(e => e.Id);

        builder.OwnsOne(e => e.FirstName, name =>
        {
            name.Property(n => n.Value).HasColumnName("first_name");
        });

        builder.OwnsOne(e => e.LastName, name =>
        {
            name.Property(n => n.Value).HasColumnName("last_name");
        });

        builder.OwnsOne(e => e.Email, name =>
        {
            name.Property(n => n.Value).HasColumnName("email");
        });

        builder.OwnsOne(e => e.Username, name =>
        {
            name.Property(n => n.Value).HasColumnName("username");
        });

        //builder.HasData(User.Create(Email.Create("test@test.com").Value, Username.Create("UserName").Value, "", DateTime.UtcNow));
    }
}
