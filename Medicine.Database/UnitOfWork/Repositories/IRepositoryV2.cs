namespace Medicine.Database.UnitOfWork.Repositories;

using System.Linq.Expressions;
using Enteties;
using Medicine.Database.UnitOfWork.Repositories.Specifications;

public interface IRepositoryV2<TDatabase>
    where TDatabase : class, IDatabaseEntity
{
    Task<TDatabase?> Find(Expression<Func<TDatabase, bool>> predicate, CancellationToken token);
    Task<TDatabase?> Find(Guid id, CancellationToken token);

    void Add(TDatabase entity);
    void Update(TDatabase entity);
    void Delete(TDatabase entity);

    Task<IReadOnlyList<TDatabase>> ListAsync(ISpecification<TDatabase> specification);
}
