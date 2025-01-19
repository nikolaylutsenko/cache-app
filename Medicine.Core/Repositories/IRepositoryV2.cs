using System.Linq.Expressions;
using Medicine.Domain.Aggregates;

namespace Medicine.Domain.Repositories;

public interface IRepositoryV2<TAggregate, TIdentity>
    where TAggregate : AggregateRoot<TIdentity>
    where TIdentity : IdentityBase
{
    IQueryable<TAggregate> AsQueryable();

    Task<TAggregate?> FindOneAsync(Expression<Func<TAggregate, bool>> predicate);

    void Update(TAggregate aggregate);
    void Remove(TAggregate aggregate);
    void Add(TAggregate aggregate);
}
