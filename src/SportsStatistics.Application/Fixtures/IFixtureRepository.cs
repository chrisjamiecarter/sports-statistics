using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures;

public interface IFixtureRepository
{
    Task<bool> CreateAsync(Fixture fixture, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Fixture fixture, CancellationToken cancellationToken);
    Task<Fixture?> GetByIdAsync(EntityId id, CancellationToken cancellationToken);
}
