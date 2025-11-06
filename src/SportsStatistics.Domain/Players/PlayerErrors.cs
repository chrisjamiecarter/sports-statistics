using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public static class PlayerErrors
{
    public static Error InvalidPosition(string position) => Error.Failure(
        "Player.InvalidPosition",
        $"A player cannot have a position of '{position}'.");

    public static Error NotCreated(string name, DateOnly dateOfBirth) => Error.Failure(
        "Player.NotCreated",
        $"The player with the Name = '{name}' and Date of Birth = '{dateOfBirth}' was not created.");
    
    public static Error NotDeleted(Guid id) => Error.Failure(
        "Player.NotDeleted",
        $"The player with the Id = '{id}' was not deleted.");

    public static Error NotFound(Guid id) => Error.NotFound(
        "Player.NotFound",
        $"The player with the Id = '{id}' was not found.");

    public static Error NotUpdated(Guid id) => Error.Failure(
        "Player.NotUpdated",
        $"The player with the Id = '{id}' was not updated.");

    public static Error SquadNumberTaken(int squadNumber) => Error.Failure(
        "Player.SquadNumberTaken",
        $"The Squad Number = '{squadNumber}' is already taken by another player.");
}
