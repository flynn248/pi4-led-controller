using Led.SharedKernal.DDD;

namespace Led.Domain.Devices.Events;

public sealed record DeviceHostnameUpdatedDomainEvent(Guid Id) : IDomainEvent;
