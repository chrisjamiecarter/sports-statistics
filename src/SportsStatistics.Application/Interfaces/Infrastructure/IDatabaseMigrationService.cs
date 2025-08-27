using SportsStatistics.Application.Models;

namespace SportsStatistics.Application.Interfaces.Infrastructure;

public interface IDatabaseMigrationService
{
    Task<MigrationResult> MigrateAsync(CancellationToken cancellationToken = default);
}
