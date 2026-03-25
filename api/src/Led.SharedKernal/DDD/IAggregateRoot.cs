namespace Led.SharedKernal.DDD;

public interface IAggregateRoot
{
    void ClearDomainEvents();
    IReadOnlyList<IDomainEvent> GetDomainEvents();
}