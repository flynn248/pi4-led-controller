namespace Led.SharedKernal.UoW;

public interface IUnitOfWork : IDisposable
{
    Guid Id { get; }
    bool IsDisposed { get; }
    bool IsCompleted { get; }
    IServiceProvider ServiceProvider { get; }

    event EventHandler<UnitOfWorkEventArgs> Disposed;

    IDatabase? GetDatabase(string key);
    void AddDatabase(string key, IDatabase database);
    Task SaveChanges(CancellationToken cancellationToken = default);
    Task Complete(CancellationToken cancellationToken = default);
    Task Rollback(CancellationToken cancellationToken = default);
}
