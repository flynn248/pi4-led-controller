using Led.SharedKernal.UoW;
using Microsoft.EntityFrameworkCore;

namespace Led.Infrastructure.Database.Abstraction;

internal sealed class EfDatabase : IDatabase
{
    public DbContext DbContext { get; }

    public EfDatabase(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return DbContext.SaveChangesAsync(cancellationToken);
    }
}
