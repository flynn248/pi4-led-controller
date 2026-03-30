using Led.Domain.Scenes;
using Led.Domain.Schedules;
using Led.Domain.Schedules.ValueObjects;
using Led.Domain.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("schedule");

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(e => e.TenantId)
            .HasColumnName("tenant_id");

        builder.HasOne<Tenant>()
            .WithMany()
            .HasForeignKey(e => e.TenantId);

        builder.Property(e => e.SceneId)
            .HasColumnName("scene_id");

        builder.HasOne<Scene>()
            .WithMany()
            .HasForeignKey(e => e.SceneId);

        builder.OwnsOne(e => e.Label, label =>
        {
            label.Property(l => l.Value)
                .HasColumnName("label")
                .HasMaxLength(ScheduleLabel.MaxLength);
        });

        builder.Property(e => e.ScheduleTypeId)
            .HasColumnName("schedule_type_id")
            .HasConversion<short>();

        builder.HasOne<ScheduleType>()
            .WithMany()
            .HasForeignKey(e => e.ScheduleTypeId);

        builder.Property(e => e.RunAtUtc)
            .HasColumnName("run_at_utc");

        builder.OwnsOne(e => e.CronExpression, cron =>
        {
            cron.Property(e => e.Value)
                .HasColumnName("cron_expression")
                .HasMaxLength(CronExpression.MaxLength);
        });

        builder.Property(e => e.IsEnabled)
            .HasColumnName("is_enabled")
            .HasDefaultValue(false);

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnName("created_at_utc");

        builder.Property(e => e.ModifiedAtUtc)
            .HasColumnName("modified_at_utc");
    }
}
