namespace Led.SharedKernal.DDD;

public abstract class Entity<TId>
    where TId : notnull
{
    protected Entity(TId id)
    {
        Id = id;
    }
    protected Entity()
    {
    }

    public TId Id { get; init; }
}
