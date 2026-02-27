namespace SportsStatistics.Web.MatchTracker;

public sealed record TeamGoalDialogResult(
    bool IsOwnGoal,
    Guid? ScorerPlayerId,
    Guid? AssistPlayerId,
    DateTime OccurredAtUtc);
