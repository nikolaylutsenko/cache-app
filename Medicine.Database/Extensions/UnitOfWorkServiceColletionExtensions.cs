using Medicine.Database.UnitOfWork;
using Medicine.Database.UnitOfWork.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Medicine.Database.Extensions;

public static class UnitOfWorkServiceCollectionExtensions
{
    public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();

        return services;
    }

    // just for fun implement injection of multiple contexts by type
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
