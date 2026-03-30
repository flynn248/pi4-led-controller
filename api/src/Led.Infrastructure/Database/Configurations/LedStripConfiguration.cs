using Led.Domain.Devices;
using Led.Domain.LedStrips;
using Led.Domain.LedStrips.ValueObjects;
using Led.Domain.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class LedStripConfiguration : IEntityTypeConfiguration<LedStrip>
{
    public void Configure(EntityTypeBuilder<LedStrip> builder)
    {
        builder.ToTable("led_strip");

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.HasKey(e => e.Id);

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

        builder.Property(e => e.LedStripTypeId)
            .HasColumnName("led_strip_type_id")
            .HasConversion<short>();

        builder.HasOne<LedStripType>()
            .WithMany()
            .HasForeignKey(e => e.LedStripTypeId);

        builder.OwnsOne(e => e.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("name")
                .HasMaxLength(LedStripName.MaxLength);
        });

        builder.OwnsOne(e => e.GpioPin, pin =>
        {
            pin.Property(p => p.Value)
               .HasColumnName("gpio_pin");
        });

        builder.OwnsOne(e => e.LedCount, count =>
        {
            count.Property(c => c.Value)
                 .HasColumnName("led_count");
        });

        builder.OwnsOne(e => e.Frequency, freq =>
        {
            freq.Property(f => f.Value)
                .HasColumnName("frequency");
        });

        builder.OwnsOne(e => e.DmaChannel, channel =>
        {
            channel.Property(c => c.Value)
                   .HasColumnName("dma_channel");
        });

        builder.OwnsOne(e => e.Brightness, bright =>
        {
            bright.Property(b => b.Value)
                  .HasColumnName("brightness");
        });

        builder.Property(e => e.Invert)
            .HasColumnName("invert");

        builder.OwnsOne(e => e.Voltage, volt =>
        {
            volt.Property(v => v.Value)
                .HasColumnName("voltage");
        });

        builder.OwnsOne(e => e.MaxCurrentMa, cur =>
        {
            cur.Property(c => c.Value)
                .HasColumnName("max_current_ma");
        });

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnName("created_at_utc");

        builder.Property(e => e.ModifiedAtUtc)
            .HasColumnName("modified_at_utc");
    }
}
