using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class PlayerDeletedDomainEventTestCase : TheoryData<Player, DateTime, PlayerDeletedDomainEvent>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    public PlayerDeletedDomainEventTestCase()
    {
        Add(Player, DateTime.UtcNow, new(Player.Id));
    }
}
