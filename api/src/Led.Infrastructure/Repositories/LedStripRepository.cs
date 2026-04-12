using Led.Domain.LedStrips;
using Led.Domain.LedStrips.Repositories;
using Led.Infrastructure.Database;
using Led.Infrastructure.Database.Abstraction;
using Led.Infrastructure.Repositories.Abstraction;

namespace Led.Infrastructure.Repositories;

internal sealed class LedStripRepository : Repository<ApplicationDbContext, LedStrip, Guid>, ILedStripRepository
{
    public LedStripRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
