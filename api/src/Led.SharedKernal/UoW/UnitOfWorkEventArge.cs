namespace Led.SharedKernal.UoW;

public sealed class UnitOfWorkEventArgs(IUnitOfWork uow) : EventArgs
{
    public IUnitOfWork UnitOfWork { get; } = uow;
}
