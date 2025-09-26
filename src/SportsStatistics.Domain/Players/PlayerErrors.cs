using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public static class PlayerErrors
{
    public static Error InvalidPosition(string position) => Error.Failure(
        "Player.InvalidPosition",
        $"A player cannot have a position of '{position}'.");

    public static Error NotCreated(EntityId id) => Error.Failure(
        "Player.NotCreated",
        $"The player with the Id = '{id}' was not created.");
    
    public static Error NotDeleted(EntityId id) => Error.Failure(
        "Player.NotDeleted",
        $"The player with the Id = '{id}' was not deleted.");

    public static Error NotFound(EntityId id) => Error.NotFound(
        "Player.NotFound",
        $"The player with the Id = '{id}' was not found.");

    public static Error NotUpdated(EntityId id) => Error.Failure(
        "Player.NotUpdated",
        $"The player with the Id = '{id}' was not updated.");
}
