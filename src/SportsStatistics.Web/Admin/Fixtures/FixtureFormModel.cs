namespace SportsStatistics.Web.Admin.Fixtures;

public class FixtureFormModel
{
    public Guid CompetitionId { get; set; }

    public string Opponent { get; set; } = string.Empty;

    public DateTime? KickoffTimeUtc { get; set; } = DateTime.UtcNow;

    public string LocationName { get; set; } = string.Empty;
}
