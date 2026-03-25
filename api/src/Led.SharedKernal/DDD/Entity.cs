namespace Led.SharedKernal.DDD;

public interface IEntity;

public abstract class Entity<TId> : IEntity
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
