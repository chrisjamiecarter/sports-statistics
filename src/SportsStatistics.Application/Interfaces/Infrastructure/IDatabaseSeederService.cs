using SportsStatistics.Shared.Results;

namespace SportsStatistics.Application.Interfaces.Infrastructure;

public interface IDatabaseSeederService
{
    Task<Result> SeedAsync(CancellationToken cancellationToken = default);
}
