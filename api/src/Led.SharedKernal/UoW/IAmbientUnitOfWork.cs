namespace Led.SharedKernal.UoW;

internal interface IAmbientUnitOfWork
{
    IUnitOfWork UnitOfWork { get; }
    void SetUnitOfWork(IUnitOfWork unitOfWork);
    IUnitOfWork? GetCurrent();
}
