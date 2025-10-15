using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public static class CompetitionErrors
{
    public static Error NotCreated(string name, string competitionTypeName) => Error.Failure(
        "Competition.NotCreated",
        $"The competition with the Name = '{name}' and Competition Type = '{competitionTypeName}' was not created.");
    
    public static Error NotDeleted(EntityId id) => Error.Failure(
        "Competition.NotDeleted",
        $"The competition with the Id = '{id}' was not deleted.");

    public static Error NotFound(EntityId id) => Error.NotFound(
        "Competition.NotFound",
        $"The competition with the Id = '{id}' was not found.");

    public static Error NotUpdated(EntityId id) => Error.Failure(
        "Competition.NotUpdated",
        $"The competition with the Id = '{id}' was not updated.");
}
