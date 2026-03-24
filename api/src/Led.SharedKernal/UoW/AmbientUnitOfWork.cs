namespace Led.SharedKernal.UoW;

internal sealed class AmbientUnitOfWork : IAmbientUnitOfWork
{
    private readonly AsyncLocal<IUnitOfWork?> _currentUow;

    public AmbientUnitOfWork()
    {
        _currentUow = new AsyncLocal<IUnitOfWork?>();
    }

    public IUnitOfWork? GetCurrent()
    {
        var current = _currentUow.Value;

        if (current is not null && (current.IsCompleted || current.IsDisposed))
        {
            return null;
        }

        return current;
    }

    public void SetUnitOfWork(IUnitOfWork unitOfWork)
    {
        _currentUow.Value = unitOfWork;
    }

    public void ClearUnitOfWork()
    {
        _currentUow.Value = null;
    }
}
