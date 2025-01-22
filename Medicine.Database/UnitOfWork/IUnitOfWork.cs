namespace Medicine.Database.UnitOfWork;

using Medicine.Database.Enteties;
using Medicine.Database.UnitOfWork.Repositories;
using Microsoft.EntityFrameworkCore;

public interface IUnitOfWork<out TContext> : IDisposable
    where TContext : DbContext
{
    IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class, IDatabaseEntity;

    Task SaveChangesAsync();

    TContext DbContext { get; }
}
