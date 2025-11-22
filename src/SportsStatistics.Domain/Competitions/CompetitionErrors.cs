using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public static class CompetitionErrors
{
    public static Error FormatNameIsRequired => Error.Validation(
        "Competition.FormatNameIsRequired",
        "The format name is required.");

    public static Error FormatNameUnknowm => Error.Validation(
        "Competition.FormatNameUnknowm",
        "The format name is unknown.");

    public static Error NameExceedsMaxLength => Error.Validation(
        "Competition.NameExceedsMaxLength",
        "The name exceeds the maximum allowed length.");

    public static Error NameIsRequired => Error.Validation(
        "Competition.NameIsRequired",
        "The name is required.");

    public static Error SeasonIdIsRequired => Error.Validation(
        "Competition.SeasonIdIsRequired",
        "The season id is required.");

    // TODO: Refactor.
    public static Error NotCreated(string name, string competitionTypeName) => Error.Failure(
        "Competition.NotCreated",
        $"The competition with the Name = '{name}' and Competition Type = '{competitionTypeName}' was not created.");
    
    public static Error NotDeleted(Guid id) => Error.Failure(
        "Competition.NotDeleted",
        $"The competition with the Id = '{id}' was not deleted.");

    public static Error NotFound(Guid id) => Error.NotFound(
        "Competition.NotFound",
        $"The competition with the Id = '{id}' was not found.");

    public static Error NotUpdated(Guid id) => Error.Failure(
        "Competition.NotUpdated",
        $"The competition with the Id = '{id}' was not updated.");

    public static class Name
    {
        public static Error ExceedsMaxLength => Error.Validation(
            "Competition.Name.ExceedsMaxLength",
            "The competition name exceeds the maximum allowed length.");

        public static Error NullOrEmpty => Error.Validation(
            "Competition.Name.NullOrEmpty",
            "The competition name cannot be null or empty.");
    }

    public static class Format
    {
        public static Error Unknown => Error.Validation(
            "Competition.Format.Unknown",
            "The competition format cannot be inferred from the name.");
    }
}
