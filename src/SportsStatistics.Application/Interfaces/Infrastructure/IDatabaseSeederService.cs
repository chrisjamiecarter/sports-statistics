using SportsStatistics.Common.Primitives.Results;

namespace SportsStatistics.Application.Interfaces.Infrastructure;

public interface IDatabaseSeederService
{
    Task<Result> SeedAsync(CancellationToken cancellationToken);
}
