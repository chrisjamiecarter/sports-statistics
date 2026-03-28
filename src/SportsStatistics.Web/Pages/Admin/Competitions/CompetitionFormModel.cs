using SportsStatistics.Web.Pages.Admin.Seasons;

namespace SportsStatistics.Web.Pages.Admin.Competitions;

public class CompetitionFormModel
{
    public SeasonDto? Season { get; set; }

    public string Name { get; set; } = string.Empty;

    public FormatOptionDto? Format { get; set; }
}
