using Led.Domain.LedStrips;
using Led.Domain.LedStrips.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class LedStripTypeConfiguration : IEntityTypeConfiguration<LedStripType>
{
    public void Configure(EntityTypeBuilder<LedStripType> builder)
    {
        builder.ToTable("led_strip_type");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasConversion<short>();

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(LedStripType.NameMaxLength);

        builder.HasData(GetSeedData());
    }

    private LedStripType[] GetSeedData()
    {
        return
        [
            LedStripType.Create(LedStripTypeId.SK6812_RGBW , "SK6812 RGBW")
        ];
    }
}
