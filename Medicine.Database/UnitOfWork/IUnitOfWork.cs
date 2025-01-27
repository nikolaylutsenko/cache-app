namespace Medicine.Database.UnitOfWork;

using Enteties;
using Repositories;

public interface IUnitOfWork : IDisposable
{
    IRepositoryV2<TEntity> GetRepository<TEntity>()
        where TEntity : class, IDatabaseEntity;

    Task SaveChangesAsync();
}
