using Led.Domain.EffectTypes.ValueObjects;
using Led.Domain.Scenes.Events;
using Led.Domain.Shared.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.EffectTypes;

public sealed class EffectType : AggregateRoot<Guid>
{
    public Guid? TenantId { get; private set; }
    public EffectTypeName Name { get; private set; }
    public EffectTypeDescription Description { get; private set; }
    public bool IsBuiltin { get; private set; }
    public bool IsImplemented { get; private set; }
    public PosNum<int> SchemaVersion { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? ModifiedAtUtc { get; private set; }

    private EffectType()
    { }

    private EffectType(Guid id, Guid tenantId, EffectTypeName name, EffectTypeDescription description, bool isBuiltin, bool isImplemented, PosNum<int> schemaVersion, DateTime createdAtUtc)
        : base(id)
    {
        TenantId = tenantId;
        Name = name;
        Description = description;
        IsBuiltin = isBuiltin;
        IsImplemented = isImplemented;
        SchemaVersion = schemaVersion;
        CreatedAtUtc = createdAtUtc;
    }

    public static EffectType Create(Guid tenantId, EffectTypeName name, EffectTypeDescription description, bool isBuiltin, bool isImplemented, PosNum<int> schemaVersion, DateTime createdAtUtc)
    {
        return new EffectType(Guid.CreateVersion7(), tenantId, name, description, isBuiltin, isImplemented, schemaVersion, createdAtUtc);
    }

    public void UpdateSchemaVersion(PosNum<int> newSchemaVersion, DateTime modifiedAtUtc)
    {
        if (SchemaVersion == newSchemaVersion)
        {
            return;
        }

        RaiseDomainEvent(new EffectTypeSchemaUpdatedDomainEvent(TenantId, Id, newSchemaVersion.Value));

        ModifiedAtUtc = modifiedAtUtc;
    }
}
