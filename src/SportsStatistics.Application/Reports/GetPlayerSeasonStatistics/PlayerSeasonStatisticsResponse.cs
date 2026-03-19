using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Reports.GetPlayerSeasonStatistics;

public sealed record PlayerSeasonStatisticsResponse(
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
    int FoulConceededCount,
    int YellowCardCount,
    int RedCardCount);
