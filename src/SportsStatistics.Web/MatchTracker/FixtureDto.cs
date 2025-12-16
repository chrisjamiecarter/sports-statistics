namespace SportsStatistics.Web.MatchTracker;

public sealed record FixtureDto(Guid Id,
                                Guid CompetitionId,
                                string CompetitionName,
                                string Opponent,
                                DateTime KickoffTimeUtc,
                                int LocationId,
                                string Location,
                                int HomeGoals,
                                int AwayGoals,
                                int StatusId,
                                string Status)
{
    public override string ToString() => $"{KickoffTimeUtc:yyyy-MM-dd HH:mm} - {Opponent} - {CompetitionName}";
}
