using Led.Domain.Tenants;
using Led.Domain.Tenants.Repositories;
using Led.Infrastructure.Database;
using Led.Infrastructure.Database.Abstraction;
using Led.Infrastructure.Repositories.Abstraction;

namespace Led.Infrastructure.Repositories;

internal sealed class TenantRespository : Repository<ApplicationDbContext, Tenant, Guid>, ITenantRepository
{
    public TenantRespository(IDbContextProvider<ApplicationDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
