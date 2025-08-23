using SportsStatistics.Application.Models;

namespace SportsStatistics.Application.Interfaces;

public interface IDatabaseMigrationService
{
    Task<MigrationResult> MigrateAsync(CancellationToken cancellationToken = default);
}
