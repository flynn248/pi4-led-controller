using Led.Domain.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class TenantUserConfiguration : IEntityTypeConfiguration<TenantUser>
{
    public void Configure(EntityTypeBuilder<TenantUser> builder)
    {
        builder.ToTable("tenant_user");

        builder.HasKey(e => new { e.TenantId, e.UserId });

        builder.HasOne<Tenant>()
            .WithMany()
            .HasForeignKey(e => e.TenantId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.UserId);

        builder.Property(e => e.TenantId)
            .HasColumnName("tenant_id");

        builder.Property(e => e.UserId)
            .HasColumnName("user_id");

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnName("created_at_utc");
    }
}
