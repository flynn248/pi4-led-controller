using Microsoft.EntityFrameworkCore;

namespace Led.Infrastructure.Database.Abstraction;

internal interface IDbContextProvider<out TDbContext>
    where TDbContext : DbContext
{
    TDbContext GetDbContext();
}
