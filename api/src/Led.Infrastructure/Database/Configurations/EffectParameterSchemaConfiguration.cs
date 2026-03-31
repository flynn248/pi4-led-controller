using Led.Domain.EffectTypes;
using Led.Domain.EffectTypes.ValueObjects;
using Led.Domain.Scenes.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class EffectParameterSchemaConfiguration : IEntityTypeConfiguration<EffectParameterSchema>
{
    public void Configure(EntityTypeBuilder<EffectParameterSchema> builder)
    {
        builder.ToTable("effect_parameter_schema");

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.EffectTypeId)
            .HasColumnName("effect_type_id");

        builder.HasOne<EffectType>()
            .WithMany()
            .HasForeignKey(e => e.EffectTypeId);

        builder.HasIndex(e => new { e.Id, e.EffectTypeId })
            .IsUnique();

        builder.OwnsOne(e => e.Key, key =>
        {
            key.Property(e => e.Value)
                .HasColumnName("key")
                .HasMaxLength(ParameterKey.MaxLength);
        });

        builder.Property(e => e.DataTypeId)
            .HasColumnName("parameter_data_type_id")
            .HasConversion<short>();

        builder.HasOne<ParameterDataType>()
            .WithMany()
            .HasForeignKey(e => e.DataTypeId);

        builder.Property(e => e.IsRequired)
            .HasColumnName("is_required");

        builder.Property(e => e.MinValue)
            .HasColumnName("min_value")
            .HasDefaultValue(null);

        builder.Property(e => e.MaxValue)
            .HasColumnName("max_value")
            .HasDefaultValue(null);

        builder.OwnsOne(e => e.AllowedValues, allow =>
        {
            allow.Property(e => e.Value)
                .HasColumnName("allowed_values")
                .HasMaxLength(ParameterAllowedValues.MaxLength)
                .HasDefaultValue(null);
        });

        builder.OwnsOne(e => e.Description, desc =>
        {
            desc.Property(e => e.Value)
                .HasColumnName("description")
                .HasMaxLength(ParameterDescription.MaxLength);
        });
    }
}
