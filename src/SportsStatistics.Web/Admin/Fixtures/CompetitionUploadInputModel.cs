using SportsStatistics.Web.Admin.Competitions;

namespace SportsStatistics.Web.Admin.Fixtures;

public sealed class CompetitionUploadInputModel
{
    public string? Name { get; set; }
    public int? FormatId { get; set; }
    public List<FixtureUploadInputModel> Fixtures { get; set; } = [];
}
