using Microsoft.Extensions.DependencyInjection;

namespace Led.SharedKernal.UoW;

internal class UnitOfWorkManager : IUnitOfWorkManager
{
    private readonly IAmbientUnitOfWork _ambientUnitOfWork;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public IUnitOfWork? Current => _ambientUnitOfWork.GetCurrent();

    public UnitOfWorkManager(IAmbientUnitOfWork ambientUnitOfWork, IServiceScopeFactory serviceScopeFactory)
    {
        _ambientUnitOfWork = ambientUnitOfWork;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public IUnitOfWork Begin(bool createNewUow = false)
    {
        var current = Current;

        if (current is not null && !createNewUow)
        {
            return new ChildUnitOfWork(current);
        }

        var unitOfWork = CreateNewUnitOfWork();

        return unitOfWork;
    }

    private IUnitOfWork CreateNewUnitOfWork()
    {
        var scope = _serviceScopeFactory.CreateScope();

        try
        {
            var parentUow = _ambientUnitOfWork.UnitOfWork;

            var newUow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            newUow.SetParent(parentUow);

            _ambientUnitOfWork.SetUnitOfWork(newUow);

            newUow.Disposed += (sender, args) =>
            {
                _ambientUnitOfWork.SetUnitOfWork(parentUow);
                scope.Dispose();
            };

            return newUow;
        }
        catch
        {
            scope.Dispose();
            throw;
        }
    }
}
