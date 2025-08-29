using SportsStatistics.Core.Results;

namespace SportsStatistics.Application.Interfaces.Infrastructure;

public interface IDatabaseSeederService
{
    Task<Result> SeedAsync(CancellationToken cancellationToken = default);
}
