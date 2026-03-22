using System.Collections.Immutable;

namespace Led.SharedKernal.UoW;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly Dictionary<string, IDatabase> _databases;
    private bool _isCompleting;
    private bool _isRolledBack;

    public Guid Id => Guid.NewGuid();
    public IUnitOfWork? ParentUow { get; private set; }
    public bool IsDisposed { get; private set; }
    public bool IsCompleted { get; private set; }
    public IServiceProvider ServiceProvider { get; }

    public event EventHandler<UnitOfWorkEventArgs>? Disposed;

    public UnitOfWork(IServiceProvider serviceProvider)
    {
        _databases = new();
        ServiceProvider = serviceProvider;
    }

    public void SetParent(IUnitOfWork parent)
    {
        ParentUow = parent;
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        if (_isRolledBack)
        {
            return;
        }

        foreach (var database in _databases.Values.ToImmutableList())
        {
            await database.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task Complete(CancellationToken cancellationToken = default)
    {
        if (_isRolledBack)
        {
            return;
        }

        ThrowIfComplete();

        _isCompleting = true;

        await SaveChanges(cancellationToken);

        IsCompleted = true;
    }

    public async Task Rollback(CancellationToken cancellationToken = default)
    {
        if (_isRolledBack)
        {
            return;
        }

        _isRolledBack = true;

        // If transactions are added in, can toss them into here to rollback. This project is unlikely to use transactions so not adding it in now
    }

    public void Dispose()
    {
        if (IsDisposed)
        {
            return;
        }

        IsDisposed = true;

        Disposed?.Invoke(this, new UnitOfWorkEventArgs(this));
    }

    #region Database Operations
    public IDatabase? GetDatabase(string key)
    {
        return _databases.GetValueOrDefault(key);
    }

    public void AddDatabase(string key, IDatabase database)
    {
        if (_databases.ContainsKey(key))
        {
            throw new InvalidOperationException($"Database already exists for key {key}");
        }

        _databases.Add(key, database);
    }
    #endregion

    private void ThrowIfComplete()
    {
        if (IsCompleted || _isCompleting)
        {
            throw new InvalidOperationException("Complete already called for Unit of Work. Cannot call again!");
        }
    }
}
