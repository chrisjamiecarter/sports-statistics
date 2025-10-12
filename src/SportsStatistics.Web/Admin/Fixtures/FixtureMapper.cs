using SportsStatistics.Application.Fixtures.Create;
using SportsStatistics.Application.Fixtures.Delete;
using SportsStatistics.Application.Fixtures.GetAll;
using SportsStatistics.Application.Fixtures.Update;

namespace SportsStatistics.Web.Admin.Fixtures;

internal static class FixtureMapper
{
    public static CreateFixtureCommand ToCreateCommand(this FixtureFormModel fixture)
        => new(fixture.CompetitionId,
               fixture.Opponent,
               fixture.KickoffTimeUtc.GetValueOrDefault(),
               fixture.LocationName);

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

    public static FixtureFormModel ToFormModel(this FixtureDto? fixture, Guid competitionId)
    {
        return fixture is null
            ? new()
            {
                CompetitionId = competitionId,
            }
            : new()
            {
                CompetitionId = competitionId,
                Opponent = fixture.Opponent,
                KickoffTimeUtc = fixture.KickoffTimeUtc,
                LocationName = fixture.LocationName,
            };
    }

    public static IQueryable<FixtureDto> ToQueryable(this List<FixtureResponse> fixtures)
        => fixtures.Select(ToDto).AsQueryable();

    // TODO: Update Command Parameters.
    public static UpdateFixtureCommand ToUpdateCommand(this FixtureFormModel fixture, Guid id)
        => new(id,
               fixture.Opponent,
               fixture.KickoffTimeUtc.GetValueOrDefault(),
               fixture.LocationName);
}
