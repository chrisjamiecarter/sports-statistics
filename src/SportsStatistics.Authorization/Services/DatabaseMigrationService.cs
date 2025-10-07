using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsStatistics.Application.Interfaces.Infrastructure;
using SportsStatistics.Application.Models;
using SportsStatistics.Authorization.Database;

namespace SportsStatistics.Authorization.Services;

internal sealed class DatabaseMigrationService : IDatabaseMigrationService
{
    private readonly IdentityDbContext _dbContext
        ;
    private readonly ILogger<DatabaseMigrationService> _logger;

    public DatabaseMigrationService(IdentityDbContext dbContext, ILogger<DatabaseMigrationService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<MigrationResult> MigrateAsync(CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            _logger.LogInformation("Starting database migration process...");

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            _logger.LogInformation("Checking database connectivity...");
            var canConnect = await strategy.ExecuteAsync(async () =>
            {
                return await _dbContext.Database.CanConnectAsync(cancellationToken);
            });

            if (!canConnect)
            {
                return MigrationResult.Failure("Cannot connect to the database.");
            }

            _logger.LogInformation("Checking for pending migrations...");
            var pendingMigrations = await strategy.ExecuteAsync(async () =>
            {
                return await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
            });

            if (!pendingMigrations.Any())
            {
                return MigrationResult.UpToDate(stopwatch.Elapsed);
            }

            _logger.LogInformation("Applying pending migrations...");
            await strategy.ExecuteAsync(async () =>
            {
                await _dbContext.Database.MigrateAsync(cancellationToken);
            });

            stopwatch.Stop();

            return MigrationResult.Success(pendingMigrations.Count(), stopwatch.Elapsed);
        }
        catch (Exception exception)
        {
            stopwatch.Stop();
            return MigrationResult.Failure($"Migration failed: {exception.Message}", exception);
        }
    }
}
