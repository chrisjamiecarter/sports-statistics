using SportsStatistics.Application.Fixtures.GetAll;

namespace SportsStatistics.Web.Admin.Fixtures;

internal static class FixtureMapper
{
    public static FixtureDto ToDto(this FixtureResponse fixture)
        => new(fixture.Id,
               fixture.KickoffTimeUtc,
               fixture.LocationName,
               "TODO");

    public static IQueryable<FixtureDto> ToQueryable(this List<FixtureResponse> fixtures)
        => fixtures.Select(ToDto).AsQueryable();
}
