using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures;

internal interface IFixtureRepository
{
    Task<bool> CreateAsync(Fixture fixture, CancellationToken cancellationToken);
}
