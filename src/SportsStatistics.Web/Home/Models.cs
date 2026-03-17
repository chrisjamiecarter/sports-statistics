namespace SportsStatistics.Web.Home;

public sealed record FixtureCardDto(
    Guid FixtureId,
    string CompetitionName,
    string Opponent,
    string Location,
    DateTime KickoffTimeLocal,
    int HomeGoals,
    int AwayGoals,
    string OutcomeOrStatus);

public sealed record SeasonStatisticCardDto(
    int GamesPlayed,
    int Wins,
    int Draws,
    int Losses,
    int GoalsFor,
    int GoalsAgainst,
    int GoalDifference);

public sealed record RecentFormItemDto(
    string Result,
    int HomeGoals,
    int AwayGoals);

public sealed record RecentFormCardDto(
    List<RecentFormItemDto> Items);

public sealed record CurrentSeasonDto(
    string Name);
