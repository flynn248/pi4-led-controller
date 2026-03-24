using Microsoft.Extensions.DependencyInjection;

namespace Led.SharedKernal.UoW;

internal sealed class UnitOfWorkManager : IUnitOfWorkManager
{
    private readonly IAmbientUnitOfWork _ambientUnitOfWork;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public IUnitOfWork? Current => _ambientUnitOfWork.GetCurrent();

    public UnitOfWorkManager(IAmbientUnitOfWork ambientUnitOfWork, IServiceScopeFactory serviceScopeFactory)
    {
        _ambientUnitOfWork = ambientUnitOfWork;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public IUnitOfWork Begin()
    {
        if (Current is not null)
        {
            throw new InvalidOperationException("Unit of Work already exists");
        }

        return CreateNewUnitOfWork();
    }

    private IUnitOfWork CreateNewUnitOfWork()
    {
        var scope = _serviceScopeFactory.CreateScope();

        try
        {
            var newUow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            _ambientUnitOfWork.SetUnitOfWork(newUow);

            newUow.Disposed += (sender, args) =>
            {
                _ambientUnitOfWork.ClearUnitOfWork();
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
