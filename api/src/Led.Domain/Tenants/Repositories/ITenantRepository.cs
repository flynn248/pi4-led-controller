using Led.Domain.Abstraction;

namespace Led.Domain.Tenants.Repositories;

public interface ITenantRepository : IRepository<Tenant, Guid>
{
}
