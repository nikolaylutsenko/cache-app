namespace Medicine.Database.UnitOfWork.Repositories;

using System.Linq.Expressions;
using CacheApp.Utils.ResultPattern;

[Obsolete]
public interface IRepository<T>
{
    public Task Add(T entity, CancellationToken token);
    public Task Update(T entity, CancellationToken token);
    public Task Delete(T entity, CancellationToken token);
    public Task<Result<IEnumerable<T>>> Get(
        Expression<Func<T, bool>>? predicate,
        CancellationToken token
    );
    public Task<Result<T>> Get(Guid id, CancellationToken token);
}
