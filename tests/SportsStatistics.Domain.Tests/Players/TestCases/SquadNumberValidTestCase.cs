using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class SquadNumberValidTestCase : TheoryData<int, SquadNumber>
{
    public SquadNumberValidTestCase()
    {
        Add(SquadNumberTestData.ValidSquadNumber.Value, SquadNumberTestData.ValidSquadNumber);
    }
}
