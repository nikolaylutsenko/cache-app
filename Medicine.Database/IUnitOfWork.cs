using Medicine.Core.Repositories;
using Medicine.Database.Repositories;
using Medicine.Domain.Entities;
using Medicine.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Database;

public interface IUnitOfWork<out TContext> : IUnitOfWork
    where TContext : DbContext
{
    TContext DbContext { get; }
}

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class, IEntity;

    Task<int> SaveChangesAsync();

    Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
}

public sealed class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>
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

    public void SetAutoDetectChanges(bool value) =>
        DbContext.ChangeTracker.AutoDetectChangesEnabled = value;

    public SaveChangesResult LastSaveChangesResult { get; }

    public IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class, IEntity
    {
        _repositories ??= new Dictionary<Type, object>();

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new Repository<TEntity>(DbContext);
        }

        return (IRepository<TEntity>)_repositories[type];
    }

    public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters) =>
        DbContext.Database.ExecuteSqlRawAsync(sql, parameters);

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await DbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            LastSaveChangesResult.Exception = exception;
            return 0;
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

/// <summary>
/// Represent operation result for SaveChanges.
/// </summary>
public sealed class SaveChangesResult
{
    /// <summary>
    /// Ctor
    /// </summary>
    public SaveChangesResult() => Messages = new List<string>();

    /// <inheritdoc />
    public SaveChangesResult(string message)
        : this() => AddMessage(message);

    /// <summary>
    /// Last Exception you can find here
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    /// Is Exception occupied while last operation execution
    /// </summary>
    public bool IsOk => Exception == null;

    /// <summary>
    /// Adds new message to result
    /// </summary>
    /// <param name="message"></param>
    public void AddMessage(string message) => Messages.Add(message);

    /// <summary>
    /// List of the error should appear there
    /// </summary>
    private List<string> Messages { get; }
}
