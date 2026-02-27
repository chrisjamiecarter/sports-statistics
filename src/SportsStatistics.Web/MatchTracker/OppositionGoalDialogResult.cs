namespace SportsStatistics.Web.MatchTracker;

public sealed record OppositionGoalDialogResult(
    bool IsOwnGoal,
    Guid? ScorerPlayerId,
    DateTime OccurredAtUtc);
