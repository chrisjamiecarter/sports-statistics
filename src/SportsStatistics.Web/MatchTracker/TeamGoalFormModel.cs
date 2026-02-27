namespace SportsStatistics.Web.MatchTracker;

public sealed class TeamGoalFormModel
{
    public bool IsOwnGoal { get; set; }

    public PlayerOptionDto? GoalScorer { get; set; }

    public PlayerOptionDto? Assister { get; set; }
}
