using SportsStatistics.Domain.Players;

namespace SportsStatistics.Domain.Tests.Players;

public class SquadNumberTestData
{
    public static readonly SquadNumber ValidSquadNumber = SquadNumber.Create(1).Value;

    public static readonly int InvalidSquadNumber = -1;
}
