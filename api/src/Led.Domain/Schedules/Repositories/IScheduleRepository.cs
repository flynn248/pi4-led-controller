using Led.Domain.Abstraction;

namespace Led.Domain.Schedules.Repositories;

public interface IScheduleRepository : IRepository<Schedule, Guid>
{
}
