using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Clubs;

public static class ClubErrors
{
    public static class Name
    {
        public static Error ExceedsMaxLength => Error.Validation(
            "Club.Name.ExceedsMaxLength",
            $"The club name must be {Clubs.Name.MaxLength} characters or less.");

        public static Error NullOrEmpty => Error.Validation(
            "Club.Name.NullOrEmpty",
            "The club name cannot be null or empty.");
    }
}
