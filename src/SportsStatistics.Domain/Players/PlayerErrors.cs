using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public static class PlayerErrors
{
    public static Error NotCreated(Guid playerId) => Error.Failure(
        "Player.NotCreated",
        $"The player with the Id = '{playerId}' was not created.");
    
    public static Error NotDeleted(Guid playerId) => Error.Failure(
        "Player.NotDeleted",
        $"The player with the Id = '{playerId}' was not deleted.");

    public static Error NotFound(Guid playerId) => Error.NotFound(
        "Player.NotFound",
        $"The player with the Id = '{playerId}' was not found.");

    public static Error NotUpdated(Guid playerId) => Error.Failure(
        "Player.NotUpdated",
        $"The player with the Id = '{playerId}' was not updated.");
}
