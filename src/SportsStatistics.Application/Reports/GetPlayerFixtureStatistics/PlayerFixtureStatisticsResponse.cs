using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Reports.GetPlayerFixtureStatistics;

public sealed record PlayerFixtureStatisticsResponse(
    Guid PlayerId,
    string PlayerName,
    int SquadNumber,
    Position Position,
    bool WasStarter,
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
