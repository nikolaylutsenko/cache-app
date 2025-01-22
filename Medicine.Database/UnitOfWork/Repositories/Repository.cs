namespace Medicine.Database.UnitOfWork.Repositories;

using System.Linq.Expressions;
using CacheApp.Utils.ResultPattern;
using Medicine.Database.Enteties;
using Medicine.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

[Obsolete]
public class Repository<T>(DbContext context) : IRepository<T>
    where T : class, IDatabaseEntity
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task Add(T entity, CancellationToken token)
    {
        await _dbSet.AddAsync(entity, token);
    }

    public async Task Delete(T entity, CancellationToken token)
    {
        _dbSet.Remove(entity);
    }

    public async Task<Result<IEnumerable<T>>> Get(
        Expression<Func<T, bool>>? predicate,
        CancellationToken token
    )
    {
        IQueryable<T> query = _dbSet;
        if (predicate is not null)
            query = query.Where(predicate);

        return await query.ToListAsync(token);
    }

    public async Task<Result<T>> Get(Guid id, CancellationToken token)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public async Task Update(T entity, CancellationToken token)
    {
        _dbSet.Update(entity);
    }
}

public class RepositoryV2<TDomain, TDomainId, TDatabase>(DbContext context)
    : IRepositoryV2<TDomain, TDomainId, TDatabase>
    where TDomain : AggregateRoot<TDomainId>
    where TDomainId : IdentityBase
    where TDatabase : class, IDatabaseEntity
{
    private readonly DbSet<TDatabase> _DBSet = context.Set<TDatabase>();

    public void Add(TDomain aggregate)
    {
        TDatabase entity = Mapper.ToDatabase<TDatabase>(aggregate);
        _DBSet.Add(entity);
    }

    public Task<TDomain?> Find(Expression<Func<TDomain, bool>> predicate)
    {
        TDatabase entity = _DBSet.Find(predicate);
    }

    public void Remove(TDomain aggregate)
    {
        throw new NotImplementedException();
    }

    public void Update(TDomain aggregate)
    {
        throw new NotImplementedException();
    }
}
