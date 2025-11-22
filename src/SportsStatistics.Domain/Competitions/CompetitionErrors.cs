using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public static class CompetitionErrors
{
    public static Error CompetitionIdIsRequired => Error.Validation(
        "Competition.CompetitionIdIsRequired",
        "The competition identifier is required.");

    public static Error FormatIdIsRequired => Error.Validation(
        "Competition.FormatIdIsRequired",
        "The format identifier is required.");

    public static Error FormatNotFound => Error.Validation(
        "Competition.FormatNotFound",
        "The format with the specified identifier was not found.");

    public static Error NameExceedsMaxLength => Error.Validation(
        "Competition.NameExceedsMaxLength",
        "The name exceeds the maximum allowed length.");

    public static Error NameIsRequired => Error.Validation(
        "Competition.NameIsRequired",
        "The name is required.");

    public static Error NotFound(Guid id) => Error.NotFound(
        "Competition.NotFound",
        $"The competition with the identifier '{id}' was not found.");

    public static Error SeasonIdIsRequired => Error.Validation(
        "Competition.SeasonIdIsRequired",
        "The season identifier is required.");

    public static object AlreadyDeleted { get; internal set; }

    // TODO: Refactor.

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
