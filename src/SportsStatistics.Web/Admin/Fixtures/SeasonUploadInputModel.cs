namespace SportsStatistics.Web.Admin.Fixtures;

public sealed class SeasonUploadInputModel
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<CompetitionUploadInputModel> Competitions { get; set; } = [];
}