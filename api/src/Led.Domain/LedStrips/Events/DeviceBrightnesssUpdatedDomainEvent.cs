using Led.SharedKernal.DDD;

namespace Led.Domain.LedStrips.Events;

public sealed record DeviceBrightnesssUpdatedDomainEvent(Guid LedStripId, int Brightness) : IDomainEvent;
