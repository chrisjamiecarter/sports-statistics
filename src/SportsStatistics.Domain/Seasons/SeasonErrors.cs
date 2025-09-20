using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Seasons;

public static class SeasonErrors
{
    public static Error NotCreated(EntityId id) => Error.Failure(
        "Season.NotCreated",
        $"The season with the Id = '{id}' was not created.");
    
    public static Error NotDeleted(EntityId id) => Error.Failure(
        "Season.NotDeleted",
        $"The season with the Id = '{id}' was not deleted.");

    public static Error NotFound(EntityId id) => Error.NotFound(
        "Season.NotFound",
        $"The season with the Id = '{id}' was not found.");

    public static Error NotUpdated(EntityId id) => Error.Failure(
        "Season.NotUpdated",
        $"The season with the Id = '{id}' was not updated.");
}
