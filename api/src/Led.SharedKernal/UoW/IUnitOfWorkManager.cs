namespace Led.SharedKernal.UoW;

public interface IUnitOfWorkManager
{
    IUnitOfWork? Current { get; }
    IUnitOfWork Begin(bool createNewUow = false);
}
