using System.Linq.Expressions;
using Led.Application.Abstraction;
using Led.Infrastructure.Database.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Led.Infrastructure.Repositories.Abstraction;

internal abstract class Repository<TDbContext, TEntity> : IRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : class
{
    private readonly IDbContextProvider<TDbContext> _dbContextProvider;

    private protected Repository(IDbContextProvider<TDbContext> dbContextProvider)
    {
        ArgumentNullException.ThrowIfNull(dbContextProvider);

        _dbContextProvider = dbContextProvider;
    }

    public TEntity Add(TEntity entity)
    {
        var context = GetDbContext();
        return context.Set<TEntity>().Add(entity).Entity;
    }

    public async Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var context = GetDbContext();
        return await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
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

    protected virtual TDbContext GetDbContext()
    {
        return _dbContextProvider.GetDbContext();
    }
}
