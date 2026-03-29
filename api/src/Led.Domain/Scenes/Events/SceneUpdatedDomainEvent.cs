using Led.SharedKernal.DDD;

namespace Led.Domain.Scenes.Events;

public sealed record SceneUpdatedDomainEvent(Guid SceneId, Guid StripId) : IDomainEvent;
