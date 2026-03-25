using Led.Domain.Abstraction;

namespace Led.Domain.Tenants.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<bool> IsDuplicateEmail(Guid userId, string email, CancellationToken cancellationToken = default);
}
