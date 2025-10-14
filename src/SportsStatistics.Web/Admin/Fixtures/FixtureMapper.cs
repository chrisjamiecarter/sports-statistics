using SportsStatistics.Application.Fixtures.Create;
using SportsStatistics.Application.Fixtures.Delete;
using SportsStatistics.Application.Fixtures.GetAll;
using SportsStatistics.Application.Fixtures.Update;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Web.Admin.Competitions;

namespace SportsStatistics.Web.Admin.Fixtures;

internal static class FixtureMapper
{
    private static DateTime GetKickoffTimeUtc(this FixtureFormModel fixture)
        => fixture.KickoffDateUtc.GetValueOrDefault().Date + fixture.KickoffTimeUtc.GetValueOrDefault().TimeOfDay;

    private static string GetLocationName(this FixtureFormModel fixture)
        => fixture.Location?.Name ?? string.Empty;

    public static CreateFixtureCommand ToCreateCommand(this FixtureFormModel fixture)
        => new(fixture.Competition?.Id ?? default,
               fixture.Opponent,
               fixture.GetKickoffTimeUtc(),
               fixture.GetLocationName());

    public static DeleteFixtureCommand ToDeleteCommand(this FixtureDto fixture)
        => new(fixture.Id);

    public static FixtureDto ToDto(this FixtureResponse fixture)
        => new(fixture.Id,
               fixture.CompetitionId,
               fixture.CompetitionName,
               fixture.Opponent,
               fixture.KickoffTimeUtc,
               fixture.LocationName,
               fixture.HomeGoals,
               fixture.AwayGoals,
               fixture.StatusName);

    public static LocationOptionDto ToDto(this FixtureLocation location)
        => new(location.Id, location.Name);

    public static FixtureFormModel ToFormModel(this FixtureDto? fixture, IEnumerable<CompetitionDto> competitions, IEnumerable<LocationOptionDto> locations)
    {
        return fixture is null
            ? new()
            : new()
            {
                Competition = competitions.SingleOrDefault(c => c.Id == fixture.CompetitionId),
                Opponent = fixture.Opponent,
                KickoffDateUtc = fixture.KickoffTimeUtc,
                KickoffTimeUtc = fixture.KickoffTimeUtc,
                Location = locations.SingleOrDefault(l => string.Equals(l.Name, fixture.LocationName, StringComparison.OrdinalIgnoreCase)),
            };
    }

    public static IQueryable<FixtureDto> ToQueryable(this List<FixtureResponse> fixtures)
        => fixtures.Select(ToDto).AsQueryable();

    public static UpdateFixtureCommand ToUpdateCommand(this FixtureFormModel fixture, Guid id)
        => new(id,
               fixture.Opponent,
               fixture.GetKickoffTimeUtc(),
               fixture.GetLocationName());
}
