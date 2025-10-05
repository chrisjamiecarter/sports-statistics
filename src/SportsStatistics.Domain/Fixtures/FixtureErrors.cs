using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public static class FixtureErrors
{
    public static Error InvalidLocation(string location) => Error.Failure(
        "Fixture.InvalidLocation",
        $"A fixture cannot have a location of '{location}'.");

    public static Error NotCreated(EntityId id) => Error.Failure(
        "Fixture.NotCreated",
        $"The fixture with the Id = '{id}' was not created.");

    public static Error NotDeleted(EntityId id) => Error.Failure(
        "Fixture.NotDeleted",
        $"The fixture with the Id = '{id}' was not deleted.");

    public static Error NotFound(EntityId id) => Error.NotFound(
        "Fixture.NotFound",
        $"The fixture with the Id = '{id}' was not found.");
}
