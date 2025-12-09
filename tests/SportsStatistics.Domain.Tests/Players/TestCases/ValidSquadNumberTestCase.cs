using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ValidSquadNumberTestCase : TheoryData<int, SquadNumber>
{
    public ValidSquadNumberTestCase()
    {
        Add(SquadNumberTestData.ValidSquadNumber.Value, SquadNumberTestData.ValidSquadNumber);
    }
}
