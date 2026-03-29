using Led.Domain.LedStrips;
using Led.Domain.Scenes;
using Led.Domain.Scenes.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class EffectConfiguration : IEntityTypeConfiguration<Effect>
{
    public void Configure(EntityTypeBuilder<Effect> builder)
    {
        builder.ToTable("effect");

        builder.HasIndex(e => new { e.SceneId, e.LedStripId })
            .IsUnique();

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.SceneId)
            .HasColumnName("scene_id");

        builder.HasOne<Scene>()
            .WithMany()
            .HasForeignKey(e => e.SceneId);

        builder.Property(e => e.LedStripId)
            .HasColumnName("led_strip_id");

        builder.HasOne<LedStrip>()
            .WithMany()
            .HasForeignKey(e => e.LedStripId);

        builder.HasOne<EffectType>()
            .WithMany()
            .HasForeignKey(e => e.EffectTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.EffectTypeId)
            .HasColumnName("effect_type_id");

        builder.OwnsOne(e => e.ParameterJson, json =>
        {
            json.ToJson()
                .Property(j => j.Value)
                .HasColumnName("parameter_json")
                .HasMaxLength(EffectParameter.MaxLength);
        });

        builder.Property(e => e.ParameterJsonSchemaVersion)
            .HasColumnName("parameter_json_schema_version");

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnName("created_at_utc");

        builder.Property(e => e.ModifiedAtUtc)
            .HasColumnName("modified_at_utc");
    }
}
