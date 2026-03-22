using Led.SharedKernal.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Led.Infrastructure.Database.Abstraction;

internal class DbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
    where TDbContext : DbContext
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public DbContextProvider(IUnitOfWorkManager unitOfWorkManager)
    {
        _unitOfWorkManager = unitOfWorkManager;
    }

    public TDbContext GetDbContext()
    {
        var currentUow = _unitOfWorkManager.Current ?? throw new InvalidOperationException("Can only create a DbContext within a unit of work!");

        var key = $"{typeof(TDbContext).FullName}";
        var db = currentUow.GetDatabase(key);

        if (db is null)
        {
            var context = currentUow.ServiceProvider.GetRequiredService<TDbContext>();
            db = new EfDatabase(context);

            currentUow.AddDatabase(key, db);
        }

        return (TDbContext)((EfDatabase)db).DbContext;
    }
}
