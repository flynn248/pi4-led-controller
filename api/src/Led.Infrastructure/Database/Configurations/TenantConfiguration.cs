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

        builder.OwnsOne(e => e.Name, name =>
        {
            name.Property(n => n.Value)
                .HasMaxLength(Name.MaxLength)
                .HasColumnName(nameof(Tenant.Name));
        });
    }
}
