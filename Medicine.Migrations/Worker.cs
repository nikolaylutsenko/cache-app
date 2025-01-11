namespace Medicine.Migrations;

using System.Data;
using System.Diagnostics;
using CacheApp.Utils;
using Database;
using Database.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using OpenTelemetry.Trace;

public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime
) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = s_activitySource.StartActivity(
            "Migrating database",
            ActivityKind.Client
        );

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await EnsureDatabaseAsync(dbContext, cancellationToken);
            await RunMigrationAsync(dbContext, cancellationToken);
            await SeedTagsAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task EnsureDatabaseAsync(
        AppDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);

                // workaround because on first run dbContext.Database.MigrateAsync() doesn't create __EFMigrationsHistory table
                // don't know why
                await dbContext.Database.ExecuteSqlRawAsync(
                    """
                    CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
                        "MigrationId" character varying(150) NOT NULL,
                        "ProductVersion" character varying(32) NOT NULL,
                        CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY("MigrationId")
                    ); 
                    """,
                    cancellationToken
                );
            }
        });
    }

    private static async Task RunMigrationAsync(
        AppDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(
                cancellationToken
            );

            if (!pendingMigrations.Any())
            {
                Console.WriteLine("No pending migrations found.");
                return;
            }

            Console.WriteLine("Applying migrations...");
            await dbContext.Database.MigrateAsync(cancellationToken);
            Console.WriteLine("Migrations applied successfully.");
        });
    }

    private static async Task SeedTagsAsync(
        AppDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        if (await dbContext.Tags.AnyAsync(cancellationToken))
            return;

        //var wordSource = new WordSource();

        var tags = WordSource
            .Generate(addAdjective: true, amount: 1000)
            .Select(x => new Tag
            {
                Id = Guid.NewGuid(),
                Name = $"{x}-{Guid.NewGuid():N}",
                Version = 1L,
            });

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(
                cancellationToken
            );
            await dbContext.Tags.AddRangeAsync(tags);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}
