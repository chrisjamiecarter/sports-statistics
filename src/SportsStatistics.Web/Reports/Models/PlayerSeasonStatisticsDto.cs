using SportsStatistics.Domain.Players;

namespace SportsStatistics.Web.Reports.Models;

public sealed record PlayerSeasonStatisticsDto(
    Guid PlayerId,
    string PlayerName,
    int SquadNumber,
    Position Position,
    int GamesStarted,
    int GamesPlayed,
    int PassSuccessCount,
    int PassFailureCount,
    int ShotOnTargetCount,
    int ShotOffTargetCount,
    int GoalCount,
    int GoalAssistCount,
    int OwnGoalCount,
    int SaveCount,
    int TackleCount,
    int FoulWonCount,
    int FoulConcededCount,
    int YellowCardCount,
    int RedCardCount);
