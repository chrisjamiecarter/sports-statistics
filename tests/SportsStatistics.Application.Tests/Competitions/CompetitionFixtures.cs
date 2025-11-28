using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Tests.Competitions;

public static class CompetitionFixtures
{
    public static readonly Season Season2023_2024 =
        Season.Create(DateRange.Create(new(2023, 8, 1), new(2024, 7, 31)).Value);

    public static readonly Season Season2024_2025 =
        Season.Create(DateRange.Create(new(2024, 8, 1), new(2025, 7, 31)).Value);

    public static readonly Competition CompetitionLeague2024_2025 =
        Season2024_2025.CreateCompetition(Name.Create("Football League").Value, Format.League);

    public static readonly Competition CompetitionCup2024_2025 =
        Season2024_2025.CreateCompetition(Name.Create("Football Cup").Value, Format.Cup);
}
