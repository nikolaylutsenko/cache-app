namespace Medicine.Database.UnitOfWork.Repositories;

using System.Linq.Expressions;
using Domain.Aggregates;
using Enteties;

public interface IRepositoryV2<TDomain, TDomainId, TDatabase>
    where TDomain : AggregateRoot<TDomainId>
    where TDomainId : IdentityBase
    where TDatabase : IDatabaseEntity
{
    Task<TDomain?> Find(Expression<Func<TDomain, bool>> predicate);

    void Add(TDomain aggregate);
    void Update(TDomain aggregate);
    void Remove(TDomain aggregate);
}
