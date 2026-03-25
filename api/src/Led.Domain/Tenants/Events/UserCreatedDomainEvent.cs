using Led.SharedKernal.DDD;

namespace Led.Domain.Tenants.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
