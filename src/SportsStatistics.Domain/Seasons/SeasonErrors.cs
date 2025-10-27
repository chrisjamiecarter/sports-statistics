using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Seasons;

public static class SeasonErrors
{
    public static Error NotCreated(DateOnly startDate, DateOnly endDate) => Error.Failure(
        "Season.NotCreated",
        $"The season with the StartDate = '{startDate}' and EndDate = '{endDate}' was not created.");
    
    public static Error NotDeleted(Guid id) => Error.Failure(
        "Season.NotDeleted",
        $"The season with the Id = '{id}' was not deleted.");

    public static Error NotFound(Guid id) => Error.NotFound(
        "Season.NotFound",
        $"The season with the Id = '{id}' was not found.");

    public static Error NotUpdated(Guid id) => Error.Failure(
        "Season.NotUpdated",
        $"The season with the Id = '{id}' was not updated.");
}
