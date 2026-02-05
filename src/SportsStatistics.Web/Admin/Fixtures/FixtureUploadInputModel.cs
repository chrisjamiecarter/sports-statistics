namespace SportsStatistics.Web.Admin.Fixtures;

public sealed class FixtureUploadInputModel
{
    public SeasonUploadModel? Season { get; set; }
    public string? SeasonDisplay => Season is null ? string.Empty : $"{Season.StartDate.Year}/{Season.EndDate.Year}";
    public CompetitionUploadModel? Competition { get; set; }
    public string? CompetitionDisplay => Competition is null ? string.Empty : $"{Competition.Name} ({Competition.Format})";
    public string? Opponent { get; set; }
    public DateTime? KickoffDateUtc { get; set; }
    public DateTime? KickoffTimeUtc { get; set; }
    public LocationOptionDto? Location { get; set; }
}
