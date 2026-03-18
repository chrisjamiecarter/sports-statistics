namespace SportsStatistics.Web.Home;

public sealed record RecentFormItemDto(
    string Result,
    string Opponent,
    int HomeGoals,
    int AwayGoals);
