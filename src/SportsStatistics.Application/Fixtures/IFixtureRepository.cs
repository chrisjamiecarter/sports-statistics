using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures;

public interface IFixtureRepository
{
    Task<bool> CreateAsync(Fixture fixture, CancellationToken cancellationToken);
}
