using SportsStatistics.Domain.Players;

namespace SportsStatistics.Web.MatchTracker;

public sealed record PlayerDto(Guid Id,
                               string Name,
                               int SquadNumber,
                               string Nationality,
                               DateOnly DateOfBirth,
                               int PositionId,
                               string PositionName,
                               int Age)
{
    public bool IsGoalkeeper => PositionId == Position.Goalkeeper.Value;
}
