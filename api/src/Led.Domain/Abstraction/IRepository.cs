using System.Linq.Expressions;
using Led.SharedKernal.DDD;

namespace Led.Domain.Abstraction;

public interface IRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : notnull
{
    TEntity Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity?> GetById(TEntityId id, CancellationToken cancellationToken = default);

    Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all entities of <typeparamref name="TEntity"/>
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation</param>
    /// <returns>A <see cref="List{T}"/> of all entities</returns>
    Task<List<TEntity>> GetList(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get entities of <typeparamref name="TEntity"/> that match the <paramref name="predicate"/>
    /// </summary>
    /// <param name="predicate">Conditions to apply to filter result set</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation</param>
    /// <returns>A <see cref="List{T}"/> that match the given <paramref name="predicate"/></returns>
    Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get entities of <typeparamref name="TEntity"/> and project them into a collection of <typeparamref name="TResult"/> based on the <paramref name="selector"/>
    /// </summary>
    /// <typeparam name="TResult">The return type</typeparam>
    /// <param name="selector">Projection to perform</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation</param>
    /// <returns>A <see cref="List{T}"/></returns>
    Task<List<TResult>> GetList<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get entities of <typeparamref name="TEntity"/> that match the <paramref name="predicate"/> and project them into a collection of <typeparamref name="TResult"/> based on the <paramref name="selector"/>
    /// </summary>
    /// <typeparam name="TResult">The return type</typeparam>
    /// <param name="predicate">Conditions to apply to filter result set</param>
    /// <param name="selector">Projection to perform</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation</param>
    /// <returns>A <see cref="List{T}"/> that match the given <paramref name="predicate"/></returns>
    Task<List<TResult>> GetList<TResult>(Expression<Func<TEntity, bool>> predicate,
                                         Expression<Func<TEntity, TResult>> selector,
                                         CancellationToken cancellationToken = default);
}
