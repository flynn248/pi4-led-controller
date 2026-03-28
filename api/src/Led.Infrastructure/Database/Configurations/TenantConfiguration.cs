using Led.Domain.Tenants;
using Led.Domain.Tenants.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("tenant");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.OwnsOne(e => e.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("name")
                .HasMaxLength(Name.MaxLength);
        });

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnName("created_at_utc");

        builder.Property(e => e.ModifiedAtUtc)
            .HasColumnName("modified_at_utc");

        builder.Ignore(e => e.Users);
    }
}
