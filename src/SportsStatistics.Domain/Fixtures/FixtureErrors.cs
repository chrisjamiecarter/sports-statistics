using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public static class FixtureErrors
{
    public static Error InvalidLocation(string location) => Error.Failure(
        "Fixture.InvalidLocation",
        $"A fixture cannot have a location of '{location}'.");

    public static Error KickoffTimeOutsideSeason(DateTime kickoffTimeUtc, DateOnly startDate, DateOnly endDate) => Error.Failure(
        "Fixture.KickoffTimeOutsideSeason",
        $"A fixture's kickoff time '{kickoffTimeUtc}' cannot be outside of the season start '{startDate}' or end '{endDate}'.");

    public static Error NotCreated(string opponent, DateTime kickoffTimeUtc, string fixtureLocationName) => Error.Failure(
        "Fixture.NotCreated",
        $"The fixture with the Opponent = '{opponent}', Kickoff Time = '{kickoffTimeUtc}' and Location = '{fixtureLocationName}' was not created.");

    public static Error NotDeleted(EntityId id) => Error.Failure(
        "Fixture.NotDeleted",
        $"The fixture with the Id = '{id}' was not deleted.");

    public static Error NotFound(Guid id) => Error.NotFound(
        "Fixture.NotFound",
        $"The fixture with the Id = '{id}' was not found.");

    public static Error NotUpdated(EntityId id) => Error.Failure(
        "Fixture.NotUpdated",
        $"The fixture with the Id = '{id}' was not updated.");
}
