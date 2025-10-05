using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures.GetAll;

internal static class FixtureMapper
{
    public static List<FixtureResponse> ToResponse(this IEnumerable<Fixture> fixtures)
        => [.. fixtures.Select(ToResponse)];

    public static FixtureResponse ToResponse(this Fixture fixture)
        => new(fixture.Id.Value,
               fixture.KickoffTimeUtc,
               fixture.Competition.Id.Value,
               fixture.Competition.Name,
               fixture.Competition.Type.Name,
               fixture.Location.Name,
               fixture.Status.Name);
}
