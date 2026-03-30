using Led.Domain.Schedules;
using Led.Domain.Schedules.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class ScheduleTypeConfiguration : IEntityTypeConfiguration<ScheduleType>
{
    public void Configure(EntityTypeBuilder<ScheduleType> builder)
    {
        builder.ToTable("schedule_type");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasConversion<short>();

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(ScheduleType.NameMaxLength);

        builder.HasData(GetSeedData());
    }

    private ScheduleType[] GetSeedData()
    {
        return
        [
            ScheduleType.Create(ScheduleTypeId.OneOff, "One Off"),
            ScheduleType.Create(ScheduleTypeId.Recurring, "Recurring")
        ];
    }
}
