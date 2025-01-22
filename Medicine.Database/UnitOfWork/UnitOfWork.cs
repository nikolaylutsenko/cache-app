namespace Medicine.Database.UnitOfWork;

using Enteties;
using Microsoft.EntityFrameworkCore;
using Repositories;

public sealed class UnitOfWork<TContext>(TContext context) : IUnitOfWork<TContext>
    where TContext : DbContext
{
    private bool _disposed;
    private Dictionary<Type, object>? _repositories = [];

    public TContext DbContext { get; } =
        context ?? throw new ArgumentNullException(nameof(context));

    public SaveChangesResult LastSaveChangesResult { get; } = new SaveChangesResult();

    public IRepositoryV2<TEntity> GetRepository<TEntity>()
        where TEntity : class, IDatabaseEntity
    {
        _repositories ??= [];

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new RepositoryV2<TEntity>(DbContext);
        }

        return (IRepositoryV2<TEntity>)_repositories[type];
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
