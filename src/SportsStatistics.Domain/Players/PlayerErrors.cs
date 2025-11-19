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

    public static class DateOfBirth
    {
        public static Error BelowMinAge => Error.Validation(
            "Player.DateOfBirth.BelowMinAge",
            "The player date of birth is below the minimum allowed age.");

        public static Error NullOrEmpty => Error.Validation(
            "Player.DateOfBirth.NullOrEmpty",
            "The player date of birth cannot be null or empty.");
    }

    public static class Name
    {
        public static Error ExceedsMaxLength => Error.Validation(
            "Player.Name.ExceedsMaxLength",
            "The player name exceeds the maximum allowed length.");

        public static Error NullOrEmpty => Error.Validation(
            "Player.Name.NullOrEmpty",
            "The player name cannot be null or empty.");
    }

    public static class Nationality
    {
        public static Error ExceedsMaxLength => Error.Validation(
            "Player.Nationality.ExceedsMaxLength",
            "The player nationality exceeds the maximum allowed length.");

        public static Error NullOrEmpty => Error.Validation(
            "Player.Nationality.NullOrEmpty",
            "The player nationality cannot be null or empty.");
    }

    public static class Position
    {
        public static Error Unknown => Error.Validation(
            "Player.Position.Unknown",
            "The player position cannot be inferred from the name.");
    }

    public static class SquadNumber
    {
        public static Error AboveMaxValue => Error.Validation(
            "Player.SquadNumber.AboveMaxValue",
            "The player squad number is above the maximum allowed value.");

        public static Error BelowMinValue => Error.Validation(
            "Player.SquadNumber.BelowMinValue",
            "The player squad number is below the minimum allowed value.");

        public static Error NullOrEmpty => Error.Validation(
            "Player.SquadNumber.NullOrEmpty",
            "The player squad number cannot be null or empty.");
    }
}
