using Led.Domain.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class TenantUserConfiguration : IEntityTypeConfiguration<TenantUser>
{
    public void Configure(EntityTypeBuilder<TenantUser> builder)
    {
        builder.HasKey(e => new { e.TenantId, e.UserId });

        builder.HasOne<Tenant>()
            .WithMany()
            .HasForeignKey(e => e.TenantId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.UserId);
    }
}
