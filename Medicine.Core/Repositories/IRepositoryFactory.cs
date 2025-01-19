using Medicine.Core.Repositories;
using Medicine.Domain.Entities;

namespace Medicine.Domain.Repositories;

public interface IRepositoryFactory
{
    IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class, IEntity;
}
