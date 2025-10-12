using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures.GetAll;

internal static class FixtureMapper
{
    public static FixtureResponse ToResponse(this Fixture fixture, Competition competition)
        => new(fixture.Id.Value,
               competition.Id.Value,
               competition.Name,
               fixture.Opponent,
               fixture.KickoffTimeUtc,
               fixture.Location.Name,
               fixture.Score.HomeGoals,
               fixture.Score.AwayGoals,
               fixture.Status.Name);
}
