namespace Medicine.Database.UnitOfWork.Repositories;

using System.Linq.Expressions;
using Medicine.Database.Enteties;
using Microsoft.EntityFrameworkCore;

public class RepositoryV2<TDatabase>(DbContext context) : IRepositoryV2<TDatabase>
    where TDatabase : class, IDatabaseEntity
{
    private readonly DbSet<TDatabase> _dBSet = context.Set<TDatabase>();

    public Task<TDatabase?> Find(
        Expression<Func<TDatabase, bool>> predicate,
        CancellationToken token
    )
    {
        return _dBSet.FirstOrDefaultAsync(predicate, token);
    }

    public Task<TDatabase?> Find(Guid id, CancellationToken token)
    {
        return _dBSet.FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public void Add(TDatabase entity)
    {
        _dBSet.Add(entity);
    }

    public void Update(TDatabase entity)
    {
        _dBSet.Update(entity);
    }

    public void Delete(TDatabase entity)
    {
        _dBSet.Remove(entity);
    }
}
