using Led.Domain.Scenes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class LedStateConfiguration : IEntityTypeConfiguration<LedState>
{
    public void Configure(EntityTypeBuilder<LedState> builder)
    {
        builder.ToTable("led_state");

        builder.Property(e => e.EffectId)
            .HasColumnName("effect_id");

        builder.HasOne<Effect>()
            .WithMany()
            .HasForeignKey(e => e.EffectId);

        builder.Property(e => e.LedIndex)
            .HasColumnName("led_index");

        builder.HasKey(e => new { e.EffectId, e.LedIndex });

        builder.Property(e => e.Red)
            .HasColumnName("red");

        builder.Property(e => e.Blue)
            .HasColumnName("blue");

        builder.Property(e => e.Green)
            .HasColumnName("green");

        builder.Property(e => e.White)
            .HasColumnName("white");
    }
}
