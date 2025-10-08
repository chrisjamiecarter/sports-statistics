namespace SportsStatistics.Web.Admin.Fixtures;

public class FixtureFormModel
{
    public Guid CompetitionId { get; set; }

    public DateTime? KickoffTimeUtc { get; set; } = DateTime.UtcNow;

    public string Location { get; set; } = string.Empty;
}
