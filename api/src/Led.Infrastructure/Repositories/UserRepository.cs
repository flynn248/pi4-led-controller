using Led.Domain.Tenants;
using Led.Domain.Tenants.Repositories;
using Led.Infrastructure.Database;
using Led.Infrastructure.Database.Abstraction;
using Led.Infrastructure.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Led.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<ApplicationDbContext, User, Guid>, IUserRepository
{
    public UserRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<bool> IsDuplicateEmail(Guid userId, string email, CancellationToken cancellationToken = default)
    {
        var context = GetDbContext();

        return await context.Set<User>()
            .AnyAsync(u => u.Id != userId && u.Email.Value == email, cancellationToken);
    }

    public async Task<bool> IsDuplicateEmail(string email, CancellationToken cancellationToken = default)
    {
        var context = GetDbContext();

        return await context.Set<User>()
            .AnyAsync(u => u.Email.Value == email, cancellationToken);
    }

    public async Task<bool> IsDuplicateUsername(string username, CancellationToken cancellationToken = default)
    {
        var context = GetDbContext();

        return await context.Set<User>()
            .AnyAsync(u => u.Username.Value == username, cancellationToken);
    }
}
