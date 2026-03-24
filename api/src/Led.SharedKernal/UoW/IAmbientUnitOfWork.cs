namespace Led.SharedKernal.UoW;

internal interface IAmbientUnitOfWork
{
    void SetUnitOfWork(IUnitOfWork unitOfWork);
    void ClearUnitOfWork();
    IUnitOfWork? GetCurrent();
}
