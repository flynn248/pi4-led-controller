using Led.SharedKernal.DDD;

namespace Led.Domain.Scenes.Events;

public sealed record EffectTypeSchemaUpdatedDomainEvent(Guid? TenantId, Guid EffectTypeId, int NewSchemaVersion) : IDomainEvent;
