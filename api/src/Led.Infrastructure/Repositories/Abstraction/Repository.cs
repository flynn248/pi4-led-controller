using System.Linq.Expressions;
using Led.Domain.Abstraction;
using Led.Infrastructure.Database.Abstraction;
using Led.SharedKernal.DDD;
using Microsoft.EntityFrameworkCore;

namespace Led.Infrastructure.Repositories.Abstraction;

internal abstract class Repository<TDbContext, TEntity, TEntityId> : IRepository<TEntity, TEntityId>
    where TDbContext : DbContext
    where TEntity : Entity<TEntityId>
    where TEntityId : notnull
{
    private readonly IDbContextProvider<TDbContext> _dbContextProvider;

    private protected Repository(IDbContextProvider<TDbContext> dbContextProvider)
    {
        ArgumentNullException.ThrowIfNull(dbContextProvider);

        _dbContextProvider = dbContextProvider;
    }

    protected virtual TDbContext GetDbContext()
    {
        return _dbContextProvider.GetDbContext();
    }

    public TEntity Add(TEntity entity)
    {
        var context = GetDbContext();
        return context.Set<TEntity>().Add(entity).Entity;
    }

    public async Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var context = GetDbContext();
        return await context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    /// <inheritdoc cref="IRepository{TEntity}" />
    public async Task<List<TEntity>> GetList(CancellationToken cancellationToken = default)
    {
        var context = GetDbContext();
        return await context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
    }

    /// <inheritdoc cref="IRepository{TEntity}" />
    public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var context = GetDbContext();
        return await context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
    }

    /// <inheritdoc cref="IRepository{TEntity}" />
    public async Task<List<TResult>> GetList<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default)
    {
        var context = GetDbContext();
        return await context.Set<TEntity>().Select(selector).ToListAsync(cancellationToken);
    }

    /// <inheritdoc cref="IRepository{TEntity}" />
    public async Task<List<TResult>> GetList<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default)
    {
        var context = GetDbContext();
        return await context.Set<TEntity>().Where(predicate).Select(selector).ToListAsync(cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        var context = GetDbContext();
        context.Set<TEntity>().Remove(entity);
    }

    public void Update(TEntity entity)
    {
        var context = GetDbContext();
        context.Set<TEntity>().Update(entity);
    }

    public async Task<TEntity?> GetById(TEntityId id, CancellationToken cancellationToken = default)
    {
        var context = GetDbContext();
        return await context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var context = GetDbContext();
        return await context.Set<TEntity>().AnyAsync(predicate, cancellationToken);
    }
}
