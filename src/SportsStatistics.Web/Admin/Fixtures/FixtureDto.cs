namespace SportsStatistics.Web.Admin.Fixtures;

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
    public string DisplayStatus => Status switch
    {
        "Scheduled" => $"{KickoffTimeUtc:HH:mm}",
        "Completed" => $"{HomeGoals} - {AwayGoals}",
        "Postponed" => "P - P",
        "Cancelled" => "X - X",
        _ => Status
    };

    public DateOnly Date => DateOnly.FromDateTime(KickoffTimeUtc);
}
