using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Tests.Seasons;

public static class SeasonFixtures
{
    public static readonly Season Season2023_2024 =
        Season.Create(DateRange.Create(new(2023, 8, 1), new(2024, 7, 31)).Value);

    public static readonly Season Season2024_2025 =
        Season.Create(DateRange.Create(new(2024, 8, 1), new(2025, 7, 31)).Value);
}
