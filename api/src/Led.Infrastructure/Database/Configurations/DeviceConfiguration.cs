using Led.Domain.Devices;
using Led.Domain.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("device");

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.TenantId)
            .HasColumnName("tenant_id");

        builder.HasOne<Tenant>()
            .WithMany()
            .HasForeignKey(e => e.TenantId);

        builder.OwnsOne(e => e.Hostname, hostName =>
        {
            hostName.Property(h => h.Value)
                .HasColumnName("hostname");
        });

        builder.OwnsOne(e => e.Description, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("description");
        });

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnName("created_at_utc");

        builder.Property(e => e.ModifiedAtUtc)
            .HasColumnName("modified_at_utc");

        builder.Property(e => e.LastSeenAtUtc)
            .HasColumnName("last_seen_at_utc");
    }
}
