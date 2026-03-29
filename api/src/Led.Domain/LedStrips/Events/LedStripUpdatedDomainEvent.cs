using Led.SharedKernal.DDD;

namespace Led.Domain.LedStrips.Events;

public sealed record LedStripUpdatedDomainEvent(Guid LedStripId) : IDomainEvent;
