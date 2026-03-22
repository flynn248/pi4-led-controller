namespace Led.SharedKernal.UoW;

internal class ChildUnitOfWork : IUnitOfWork
{
    private readonly IUnitOfWork _partentUow;

    public event EventHandler<UnitOfWorkEventArgs>? Disposed;

    public Guid Id => _partentUow.Id;
    public IUnitOfWork ParentUow => _partentUow.ParentUow;
    public bool IsDisposed => _partentUow.IsDisposed;
    public bool IsCompleted => _partentUow.IsCompleted;
    public IServiceProvider ServiceProvider => _partentUow.ServiceProvider;


    public ChildUnitOfWork(IUnitOfWork parent)
    {
        _partentUow = parent;

        _partentUow.Disposed += (sender, args) =>
        {
            Disposed?.Invoke(sender, args);
        };
    }

    public void SetParent(IUnitOfWork parent)
    {
        _partentUow.SetParent(parent);
    }

    public IDatabase? GetDatabase(string key)
    {
        return _partentUow.GetDatabase(key);
    }

    public void AddDatabase(string key, IDatabase database)
    {
        _partentUow.AddDatabase(key, database);
    }

    public Task SaveChanges(CancellationToken cancellationToken = default)
    {
        return _partentUow.SaveChanges(cancellationToken);
    }

    public Task Complete(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task Rollback(CancellationToken cancellationToken = default)
    {
        return _partentUow.Rollback(cancellationToken);
    }

    public void Dispose()
    {
    }
}
