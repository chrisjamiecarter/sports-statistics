namespace SportsStatistics.Web.Admin.Fixtures;

public sealed class FixtureUploadInputModel
{
    public string? Season { get; set; }
    public string? Competition { get; set; }
    public string? Opponent { get; set; }
    public DateTime? KickoffDateUtc { get; set; }
    public DateTime? KickoffTimeUtc { get; set; }
    public LocationOptionDto? Location { get; set; }
}
