using Led.Domain.Devices;
using Led.Domain.LedStrips;
using Led.Domain.Scenes;
using Led.Domain.Scenes.ValueObjects;
using Led.Domain.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class ActiveStateConfiguration : IEntityTypeConfiguration<ActiveState>
{
    public void Configure(EntityTypeBuilder<ActiveState> builder)
    {
        builder.ToTable("active_state");

        builder.Property(e => e.LedStripId)
            .HasColumnName("led_strip_id");

        builder.HasOne<LedStrip>()
            .WithOne();

        builder.HasKey(e => e.LedStripId);

        builder.Property(e => e.TenantId)
            .HasColumnName("tenant_id");

        builder.HasOne<Tenant>()
            .WithMany()
            .HasForeignKey(e => e.TenantId);

        builder.Property(e => e.DeviceId)
            .HasColumnName("device_id");

        builder.HasOne<Device>()
            .WithMany()
            .HasForeignKey(e => e.DeviceId);

        builder.Property(e => e.SceneId)
            .HasColumnName("scene_id");

        builder.HasOne<Scene>()
            .WithMany()
            .HasForeignKey(e => e.SceneId);

        builder.OwnsOne(e => e.Source, source =>
        {
            source.Property(s => s.Value)
                .HasColumnName("source")
                .HasMaxLength(ActiveStateSource.MaxLength);
        });

        builder.Property(e => e.ActivatedAtUtc)
            .HasColumnName("activated_at_utc");
    }
}
