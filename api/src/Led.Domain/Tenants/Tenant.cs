using Led.Domain.Tenants.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.Tenants;

public sealed class Tenant : AggregateRoot<Guid>
{
    public Name Name { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? ModifiedAtUtc { get; private set; }
    public List<TenantUser> Users { get; private set; } = new();

    private Tenant()
    { }

    private Tenant(Guid id, Name name, TenantUser tenantUser, DateTime createdAtUtc)
        : base(id)
    {
        Name = name;
        CreatedAtUtc = createdAtUtc;
        Users.Add(tenantUser);
    }

    public static Tenant Create(Guid userId, Name name, DateTime createdAtUtc)
    {
        var tenantId = Guid.CreateVersion7();
        var tenantUser = new TenantUser(tenantId, userId, createdAtUtc);

        return new Tenant(tenantId, name, tenantUser, createdAtUtc);
    }

    public void UpdateName(Name name, DateTime modifiedAtUtc)
    {
        Name = name;
        ModifiedAtUtc = modifiedAtUtc;
    }

    public void AddUser(Guid userId, DateTime modifiedAtUtc)
    {
        Users.Add(new TenantUser(Id, userId, modifiedAtUtc));
    }
}
