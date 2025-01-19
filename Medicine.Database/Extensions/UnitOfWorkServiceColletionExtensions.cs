using Medicine.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Medicine.Database.Extensions;

public static class UnitOfWorkServiceCollectionExtensions
{
    public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddScoped<IRepositoryFactory, UnitOfWork<TContext>>();
        // Following has a issue: IUnitOfWork cannot support multiple dbContext/database,
        // that means cannot call AddUnitOfWork<TContext> multiple times.
        // Solution: check IUnitOfWork whether or null
        services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
        services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();

        return services;
    }

    public static IServiceCollection AddUnitOfWork(
        this IServiceCollection services,
        params Type[] types
    )
    {
        foreach (var type in types)
        {
            if (!typeof(DbContext).IsAssignableFrom(type))
                throw new ArgumentException($"{type.Name} is not a DbContext type.");

            var interfaceType = typeof(IUnitOfWork<>).MakeGenericType(type);
            var implementationType = typeof(UnitOfWork<>).MakeGenericType(type);

            services.AddScoped(interfaceType, implementationType);
        }

        return services;
    }
}
