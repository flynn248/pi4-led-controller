using Led.Domain.Tenants;
using Led.Domain.Tenants.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.HasKey(e => e.Id);

        builder.OwnsOne(e => e.FirstName, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("first_name")
                .HasMaxLength(Name.MaxLength);
        });

        builder.OwnsOne(e => e.LastName, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("last_name")
                .HasMaxLength(Name.MaxLength);
        });

        builder.OwnsOne(e => e.Email, email =>
        {
            email.Property(n => n.Value)
                .HasColumnName("email")
                .HasMaxLength(Email.MaxLength);

            email.HasIndex(n => n.Value)
                .IsUnique();
        });

        builder.OwnsOne(e => e.Username, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("username")
                .HasMaxLength(Username.MaxLength);

            name.HasIndex(n => n.Value)
                .IsUnique();
        });

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnName("created_at_utc");

        builder.Property(e => e.ModifiedAtUtc)
            .HasColumnName("modified_at_utc");

        builder.Property(e => e.PasswordHash)
            .HasColumnName("password_hash");
    }
}
