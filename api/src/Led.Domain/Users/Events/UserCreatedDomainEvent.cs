using Led.SharedKernal.DDD;

namespace Led.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
