using Led.Domain.EffectTypes;
using Led.Domain.EffectTypes.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class EffectTypeConfiguration : IEntityTypeConfiguration<EffectType>
{
    private const string _tenantIdColumnName = "tenant_id";

    public void Configure(EntityTypeBuilder<EffectType> builder)
    {
        builder.ToTable("effect_type");

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.TenantId)
            .HasColumnName(_tenantIdColumnName);

        builder.OwnsOne(e => e.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("name")
                .HasMaxLength(EffectTypeName.MaxLength);

            name.Property<Guid>(nameof(EffectType.TenantId))
                .HasColumnName(_tenantIdColumnName);

            name.HasIndex(nameof(EffectType.TenantId), nameof(EffectTypeName.Value))
                .IsUnique();

            name.HasIndex(nameof(EffectType.TenantId), nameof(EffectTypeName.Value))
                .IsUnique()
                .HasFilter($"WHERE {_tenantIdColumnName} IS NULL"); // For built-in types w/ NULL tenant_id
        });

        builder.OwnsOne(e => e.Description, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("description")
                .HasMaxLength(EffectTypeDescription.MaxLength);
        });

        builder.Property(e => e.IsBuiltin)
            .HasColumnName("is_builtin")
            .HasDefaultValue(false);

        builder.Property(e => e.IsImplemented)
            .HasColumnName("is_implmeneted")
            .HasDefaultValue(false);

        builder.OwnsOne(e => e.SchemaVersion, version =>
        {
            version.Property(v => v.Value)
                .HasColumnName("schema_version")
                .HasDefaultValue(1);
        });

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnName("created_at_utc");

        builder.Property(e => e.ModifiedAtUtc)
            .HasColumnName("modified_at_utc");
    }
}
