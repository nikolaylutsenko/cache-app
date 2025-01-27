namespace Medicine.Database.UnitOfWork;

using Enteties;
using Repositories;

public sealed class UnitOfWork(MedicineDbContext context) : IUnitOfWork
{
    private bool _disposed;
    private Dictionary<Type, object>? _repositories = [];

    public MedicineDbContext DbContext { get; } =
        context ?? throw new ArgumentNullException(nameof(context));

    public SaveChangesResult LastSaveChangesResult { get; } = new SaveChangesResult();

    public IRepositoryV2<TEntity> GetRepository<TEntity>()
        where TEntity : class, IDatabaseEntity
    {
        _repositories ??= [];

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new RepositoryV2<TEntity>(context);
        }

        return (IRepositoryV2<TEntity>)_repositories[type];
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await context.SaveChangesAsync();
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
                context.Dispose();
            }
        }
        _disposed = true;
    }
}
