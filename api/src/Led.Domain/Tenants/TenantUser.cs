using Led.SharedKernal.DDD;

namespace Led.Domain.Tenants;

public sealed class TenantUser : IEntity
{
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
    public DateTime CreatedAtUtc { get; init; }

    internal TenantUser(Guid tenantId, Guid userId, DateTime createdAtUtc)
    {
        TenantId = tenantId;
        UserId = userId;
        CreatedAtUtc = createdAtUtc;
    }

    private TenantUser()
    { }
}
