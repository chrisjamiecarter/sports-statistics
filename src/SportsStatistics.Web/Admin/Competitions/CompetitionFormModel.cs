using SportsStatistics.Web.Admin.Seasons;

namespace SportsStatistics.Web.Admin.Competitions;

public class CompetitionFormModel
{
    public SeasonDto? Season { get; set; }

    public string Name { get; set; } = string.Empty;

    public FormatOptionDto? Format { get; set; }
}
