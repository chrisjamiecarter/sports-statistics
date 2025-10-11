using SportsStatistics.Application.Fixtures.Create;
using SportsStatistics.Application.Fixtures.Delete;
using SportsStatistics.Application.Fixtures.GetAll;
using SportsStatistics.Application.Fixtures.Update;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Web.Admin.Seasons;

namespace SportsStatistics.Web.Admin.Fixtures;

internal static class FixtureMapper
{
    public static CreateFixtureCommand ToCreateCommand(this FixtureFormModel fixture)
        => new(fixture.CompetitionId,
               fixture.KickoffTimeUtc.GetValueOrDefault(),
               fixture.Location);

    public static DeleteFixtureCommand ToDeleteCommand(this FixtureDto fixture)
        => new(fixture.Id);

    public static FixtureDto ToDto(this FixtureResponse fixture)
        => new(fixture.Id,
               fixture.KickoffTimeUtc,
               fixture.LocationName,
               "TODO");

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
                KickoffTimeUtc = fixture.KickoffTimeUtc,
                Location = fixture.Location,
            };
    }

    public static IQueryable<FixtureDto> ToQueryable(this List<FixtureResponse> fixtures)
        => fixtures.Select(ToDto).AsQueryable();

    // TODO: Update Command Parameters.
    public static UpdateFixtureCommand ToUpdateCommand(this FixtureFormModel fixture, Guid id)
        => new(id,
               fixture.KickoffTimeUtc.GetValueOrDefault(),
               fixture.Location,
               0,
               0,
               FixtureStatus.Scheduled.Name);
}
