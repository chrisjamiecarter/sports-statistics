using SportsStatistics.Domain.Players;

namespace SportsStatistics.Domain.Tests.Players.TestData;

public static class NationalityTestData
{
    public static readonly Nationality ValidNationality = Nationality.Create(nameof(Nationality)).Value;
}
