using Medicine.Database.Enteties;
using Medicine.Database.Repositories;
using Medicine.Database.UnitOfWork.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Database.UnitOfWork;

public sealed class UnitOfWork<TContext> : IUnitOfWork<TContext>
    where TContext : DbContext
{
    private bool _disposed;
    private Dictionary<Type, object>? _repositories;

    public UnitOfWork(TContext context)
    {
        DbContext = context ?? throw new ArgumentNullException(nameof(context));
        LastSaveChangesResult = new SaveChangesResult();
    }

    public TContext DbContext { get; }

    public SaveChangesResult LastSaveChangesResult { get; }

    public IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class, IDatabaseEntity
    {
        _repositories ??= new Dictionary<Type, object>();

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new Repository<TEntity>(DbContext);
        }

        return (IRepository<TEntity>)_repositories[type];
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await DbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            LastSaveChangesResult.Exception = exception;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _repositories?.Clear();
                DbContext.Dispose();
            }
        }
        _disposed = true;
    }
}
