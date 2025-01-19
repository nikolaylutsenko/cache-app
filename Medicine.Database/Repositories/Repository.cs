namespace Medicine.Database.Repositories;

using System.Linq.Expressions;
using CacheApp.Utils.ResultPattern;
using Core.Repositories;
using Medicine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class Repository<T>(DbContext context) : IRepository<T>
    where T : class, IEntity
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
