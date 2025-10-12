namespace SportsStatistics.Web.Admin.Fixtures;

public sealed record FixtureDto(Guid Id,
                                Guid CompetitionId,
                                string CompetitionName,
                                string Opponent,
                                DateTime KickoffTimeUtc,
                                string LocationName,
                                int HomeGoals,
                                int AwayGoals,
                                string StatusName)
{
    public string DisplayStatus => StatusName switch
    {
        "Scheduled" => $"{KickoffTimeUtc:HH:mm}",
        "Completed" => $"{HomeGoals} - {AwayGoals}",
        "Postponed" => "P - P",
        "Cancelled" => "X - X",
        _ => StatusName
    };

    public DateOnly Date => DateOnly.FromDateTime(KickoffTimeUtc);
}
