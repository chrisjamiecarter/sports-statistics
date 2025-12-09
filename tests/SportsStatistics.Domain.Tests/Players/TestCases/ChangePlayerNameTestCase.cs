using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ChangePlayerNameTestCase : TheoryData<Player, Name>
{
    public ChangePlayerNameTestCase()
    {
        Add(PlayerTestData.ValidPlayerWithName(Name.Create("Old Name").Value),
            Name.Create("New Name").Value);
    }
}
