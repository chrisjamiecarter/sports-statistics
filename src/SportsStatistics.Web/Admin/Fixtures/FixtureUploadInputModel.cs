namespace SportsStatistics.Web.Admin.Fixtures;

public sealed class FixtureUploadInputModel
{
    public string? Opponent { get; set; }
    public DateTime? KickoffTimeUtc { get; set; }
    public LocationOptionDto? Location { get; set; }
}
