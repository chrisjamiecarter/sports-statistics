using SportsStatistics.Application.Fixtures.GetCompletedBySeasonId;

namespace SportsStatistics.Web.Reports.Models;

internal static class FixtureMapper
{
    public static IQueryable<FixtureDto> ToQueryable(this List<FixtureResponse> fixtures) =>
        fixtures.Select(ToDto).AsQueryable();

    public static FixtureDto ToDto(this FixtureResponse fixture) => new(
        fixture.Id,
        fixture.CompetitionId,
        fixture.CompetitionName,
        fixture.Opponent,
        fixture.KickoffTimeUtc,
        fixture.Location,
        fixture.Score,
        fixture.Status,
        fixture.Outcome);
}
