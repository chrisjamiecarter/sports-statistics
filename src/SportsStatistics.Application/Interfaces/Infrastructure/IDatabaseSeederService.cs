using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Interfaces.Infrastructure;

public interface IDatabaseSeederService
{
    Task<Result> SeedAsync(CancellationToken cancellationToken);
}
