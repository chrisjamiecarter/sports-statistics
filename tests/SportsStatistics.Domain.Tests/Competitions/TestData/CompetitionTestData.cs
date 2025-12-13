using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Seasons.TestData;

namespace SportsStatistics.Domain.Tests.Competitions.TestData;

public static class CompetitionTestData
{
    public static Season Season => SeasonTestData.ValidSeason;

    public static Name Name => NameTestData.ValidName;

    public static Format Format => FormatTestData.ValidFormat;

    public static Competition ValidCompetition => Competition.Create(Season, Name, Format);
}
