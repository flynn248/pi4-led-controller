namespace Led.SharedKernal.UoW;

internal class AmbientUnitOfWork : IAmbientUnitOfWork
{
    private readonly AsyncLocal<IUnitOfWork> _currentUow;

    public IUnitOfWork UnitOfWork => _currentUow.Value!;

    public AmbientUnitOfWork()
    {
        _currentUow = new AsyncLocal<IUnitOfWork>();
    }

    public IUnitOfWork? GetCurrent()
    {
        var current = UnitOfWork;

        while (current is not null && (current.IsCompleted || current.IsDisposed))
        {
            current = current.ParentUow;
        }

        return current;
    }

    public void SetUnitOfWork(IUnitOfWork unitOfWork)
    {
        _currentUow.Value = unitOfWork;
    }
}
