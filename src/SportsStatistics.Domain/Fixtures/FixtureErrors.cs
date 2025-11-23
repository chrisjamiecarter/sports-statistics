using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public static class FixtureErrors
{
    public static Error AlreadyDeleted => Error.Validation(
        "Fixture.AlreadyDeleted",
        "The fixture has already been deleted.");

    public static Error AlreadyScheduledOnDate(DateOnly date) => Error.Conflict(
        "Fixture.AlreadyScheduledOnDate",
        $"A fixture is already scheduled on {date:yyyy-MM-dd}. Only one fixture per day is allowed.");

    public static Error CompetitionIdIsRequired => Error.Validation(
        "Fixture.CompetitionIdIsRequired",
        "The competition identifier is required.");

    public static Error FixtureIdIsRequired => Error.Validation(
        "Fixture.FixtureIdIsRequired",
        "The fixture identifier is required.");

    public static Error InvalidLocation(string location) => Error.Failure(
        "Fixture.InvalidLocation",
        $"A fixture cannot have a location of '{location}'.");

    public static Error KickoffDateAndTimeIsRequired => Error.Validation(
        "Fixture.KickoffDateAndTimeIsRequired",
        "The kickoff date and time of the fixture is required.");

    public static Error KickoffTimeOutsideSeason(DateTime kickoffTimeUtc, DateOnly startDate, DateOnly endDate) => Error.Failure(
        "Fixture.KickoffTimeOutsideSeason",
        $"The fixture's kickoff time '{kickoffTimeUtc}' must be inside of the season '{startDate}/{endDate}'.");

    public static Error LocationIdIsRequired => Error.Validation(
        "Fixture.LocationIdIsRequired",
        "The location identifier is required.");

    public static Error LocationNotFound => Error.Validation(
        "Fixture.LocationNotFound",
        "The location with the specified identifier was not found.");

    public static Error NotCreated(string opponent, DateTime kickoffTimeUtc, string fixtureLocationName) => Error.Failure(
        "Fixture.NotCreated",
        $"The fixture with the Opponent = '{opponent}', Kickoff Time = '{kickoffTimeUtc}' and Location = '{fixtureLocationName}' was not created.");

    public static Error NotDeleted(Guid id) => Error.Failure(
        "Fixture.NotDeleted",
        $"The fixture with the Id = '{id}' was not deleted.");

    public static Error NotFound(Guid id) => Error.NotFound(
        "Fixture.NotFound",
        $"The fixture with the Id = '{id}' was not found.");

    public static Error NotUpdated(Guid id) => Error.Failure(
        "Fixture.NotUpdated",
        $"The fixture with the Id = '{id}' was not updated.");

    public static Error OpponentExceedsMaxLength => Error.Validation(
        "Fixture.OpponentExceedsMaxLength",
        "The opponent exceeds the maximum allowed length.");

    public static Error OpponentIsRequired => Error.Validation(
        "Fixture.OpponentIsRequired",
        "The opponent is required.");

    public static class KickoffTimeUtc
    {
        public static Error NullOrEmpty => Error.Validation(
            "Fixture.Opponent.KickoffTimeUtc",
            "The fixture kick off time cannot be null or empty.");
    }

    public static class Location
    {
        public static Error Unknown => Error.Validation(
            "Fixture.Location.Unknown",
            "The fixture location cannot be inferred from the name.");
    }

    public static class Opponent
    {
        public static Error ExceedsMaxLength => Error.Validation(
            "Fixture.Opponent.ExceedsMaxLength",
            "The fixture opponent exceeds the maximum allowed length.");

        public static Error NullOrEmpty => Error.Validation(
            "Fixture.Opponent.NullOrEmpty",
            "The fixture opponent cannot be null or empty.");
    }

    public static class Status
    {
        public static Error Unknown => Error.Validation(
            "Fixture.Status.Unknown",
            "The fixture status cannot be inferred from the name.");
    }
}
