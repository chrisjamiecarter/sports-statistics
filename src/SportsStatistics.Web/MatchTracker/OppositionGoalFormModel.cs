namespace SportsStatistics.Web.MatchTracker;

public sealed class OppositionGoalFormModel
{
    public bool IsOwnGoal { get; set; }

    public PlayerOptionDto? GoalScorer { get; set; }
}
