using SportsStatistics.Web.Admin.Competitions;

namespace SportsStatistics.Web.Admin.Fixtures;

public class FixtureFormModel
{
    public CompetitionDto? Competition { get; set; }
    
    public DateTime? KickoffDateUtc { get; set; }
    
    public DateTime? KickoffTimeUtc { get; set; }

    public LocationOptionDto? Location { get; set; }

    public string Opponent { get; set; } = string.Empty;
}
