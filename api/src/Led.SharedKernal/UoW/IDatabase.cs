namespace Led.SharedKernal.UoW;

public interface IDatabase
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
