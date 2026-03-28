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
            .HasColumnName("id");

        builder.HasKey(e => e.Id);

        builder.OwnsOne(e => e.TypeName, name =>
        {
            name.Property(e => e.TypeName)
                .HasColumnName("name")
                .HasMaxLength(LedStripTypeName.MaxLength);
        });
    }
}
