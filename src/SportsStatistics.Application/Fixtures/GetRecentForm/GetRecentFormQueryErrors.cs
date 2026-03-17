using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetRecentForm;

public static class GetRecentFormQueryErrors
{
    public static Error CountLessThanOrEqualToZero => Error.Validation(
        "GetRecentFormQuery.CountLessThanOrEqualToZero",
        "The count value must be greater than zero.");

    public static Error SeasonIdRequired => Error.Validation(
        "GetRecentFormQuery.SeasonIdRequired",
        "The season identifier is required.");
}
