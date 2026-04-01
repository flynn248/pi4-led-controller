using Led.Domain.Scenes;
using Led.Domain.Scenes.ValueObjects;
using Led.Domain.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class SceneConfiguration : IEntityTypeConfiguration<Scene>
{
    private const string _tenantIdColumnName = "tenant_id";

    public void Configure(EntityTypeBuilder<Scene> builder)
    {
        builder.ToTable("scene");

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.TenantId)
            .HasColumnName(_tenantIdColumnName);

        builder.HasOne<Tenant>()
            .WithMany()
            .HasForeignKey(e => e.TenantId);

        builder.OwnsOne(e => e.Name, name =>
        {
            name.Property(e => e.Value)
                .HasColumnName("name")
                .HasMaxLength(SceneName.MaxLength);

            name.Property<Guid>(nameof(Scene.TenantId))
                .HasColumnName(_tenantIdColumnName);

            name.HasIndex(nameof(Scene.TenantId), nameof(SceneName.Value))
                .IsUnique();
        });

        builder.OwnsOne(e => e.Description, desc =>
        {
            desc.Property(e => e.Value)
                .HasColumnName("description")
                .HasMaxLength(SceneDescription.MaxLength);
        });

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnName("created_at_utc");

        builder.Property(e => e.ModifiedAtUtc)
            .HasColumnName("modified_at_utc");
    }
}
