using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Tests.Fixtures;

public static class FixtureFixtures
{
    public static readonly Season Season2023_2024 =
        Season.Create(
            DateRange.Create(new(2023, 8, 1), new(2024, 7, 31)).Value);

    public static readonly Season Season2024_2025 =
        Season.Create(
            DateRange.Create(new(2024, 8, 1), new(2025, 7, 31)).Value);

    public static readonly Competition CompetitionLeague2024_2025 =
        Season2024_2025.CreateCompetition(
            Name.Create("Football League").Value, 
            Format.League);

    public static readonly Competition CompetitionCup2024_2025 =
        Season2024_2025.CreateCompetition(
            Name.Create("Football Cup").Value, 
            Format.Cup);

    public static readonly Fixture FixtureGW1League2024_2925 =
        CompetitionLeague2024_2025.CreateFixture(
            Opponent.Create("League Opponent One").Value, 
            KickoffTimeUtc.Create(Season2024_2025.DateRange.StartDate.ToDateTime(new(12, 30))).Value, 
            Location.Home);

    public static readonly Fixture FixtureR1Cup2024_2925 =
        CompetitionCup2024_2025.CreateFixture(
            Opponent.Create("Cup Opponent One").Value,
            KickoffTimeUtc.Create(Season2024_2025.DateRange.StartDate.AddMonths(1).ToDateTime(new(12, 30))).Value,
            Location.Away);
}
