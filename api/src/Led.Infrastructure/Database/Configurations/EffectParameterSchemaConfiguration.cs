using Led.Domain.EffectTypes;
using Led.Domain.EffectTypes.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class EffectParameterSchemaConfiguration : IEntityTypeConfiguration<EffectParameterSchema>
{
    private const string _effectTypeIdColumnName = "effect_type_id";
    private const string _parentIdColumnName = "parent_id";

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

        builder.Property(e => e.ParentId)
            .HasColumnName(_parentIdColumnName);

        builder.HasOne<EffectParameterSchema>()
            .WithMany()
            .HasForeignKey(e => e.ParentId);

        builder.OwnsOne(e => e.Key, key =>
        {
            key.Property(e => e.Value)
                .HasColumnName("key")
                .HasMaxLength(ParameterKey.MaxLength);

            key.Property<Guid>(nameof(EffectParameterSchema.EffectTypeId))
                .HasColumnName(_effectTypeIdColumnName);

            key.Property<Guid>(nameof(EffectParameterSchema.ParentId))
                .HasColumnName(_parentIdColumnName);

            key.HasIndex(nameof(EffectParameterSchema.EffectTypeId), nameof(EffectParameterSchema.ParentId), nameof(ParameterKey.Value))
                .IsUnique();
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
