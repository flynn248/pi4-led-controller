using Led.SharedKernal.DDD;

namespace Led.Domain.Scenes.Events;

public sealed record ActiveStateCreatedOrUpdatedDomainEvent(Guid LedStripId, Guid DeviceId, Guid SceneId) : IDomainEvent;
