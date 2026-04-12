using Led.Domain.Abstraction;

namespace Led.Domain.LedStrips.Repositories;

public interface ILedStripRepository : IRepository<LedStrip, Guid>
{
}
