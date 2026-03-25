using Led.SharedKernal.DDD;

namespace Led.Domain.Tenants;

public sealed class TenantUser : IEntity
{
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
    public DateTime CreatedAt { get; init; }

    internal TenantUser(Guid tenantId, Guid userId, DateTime createdAt)
    {
        TenantId = tenantId;
        UserId = userId;
        CreatedAt = createdAt;
    }

    private TenantUser()
    { }
}
