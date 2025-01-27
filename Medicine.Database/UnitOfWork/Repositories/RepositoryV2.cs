namespace Medicine.Database.UnitOfWork.Repositories;

using System.Linq.Expressions;
using Enteties;
using Medicine.Database.UnitOfWork.Repositories.Specifications;
using Microsoft.EntityFrameworkCore;

//TODO: read this - https://dotnetfullstackdev.medium.com/mastering-the-specification-pattern-in-c-d7a9f0a5e6de

public class RepositoryV2<TDatabase>(DbContext context) : IRepositoryV2<TDatabase>
    where TDatabase : class, IDatabaseEntity
{
    private readonly DbSet<TDatabase> _dBSet = context.Set<TDatabase>();

    public Task<TDatabase?> Find(
        Expression<Func<TDatabase, bool>> predicate,
        CancellationToken token
    )
    {
        return _dBSet.AsNoTracking().FirstOrDefaultAsync(predicate, token);
    }

    public Task<TDatabase?> Find(Guid id, CancellationToken token)
    {
        return _dBSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public void Add(TDatabase entity)
    {
        context.Entry(entity).State = EntityState.Added;
    }

    public void Update(TDatabase entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TDatabase entity)
    {
        context.Entry(entity).State = EntityState.Deleted;
    }

    // GENERIC EF REPOSITORY WITH SPECIFICATION
    // https://github.com/dotnet-architecture/eShopOnWeb
    public IEnumerable<TDatabase> List(ISpecification<TDatabase> spec)
    {
        // fetch a Queryable that includes all expression-based includes
        var queryableResultWithIncludes = spec.Includes.Aggregate(
            context.Set<TDatabase>().AsQueryable(),
            (current, include) => current.Include(include)
        );

        // modify the IQueryable to include any string-based include statements
        var secondaryResult = spec.IncludeStrings.Aggregate(
            queryableResultWithIncludes,
            (current, include) => current.Include(include)
        );

        // return the result of the query using the specification's criteria expression
        return secondaryResult.Where(spec.Criteria).AsEnumerable();
    }

    public async Task<IReadOnlyList<TDatabase>> ListAsync(ISpecification<TDatabase> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    private IQueryable<TDatabase> ApplySpecification(ISpecification<TDatabase> specification)
    {
        return SpecificationEvaluator<TDatabase>.GetQuery(_dBSet.AsQueryable(), specification);
    }
}
