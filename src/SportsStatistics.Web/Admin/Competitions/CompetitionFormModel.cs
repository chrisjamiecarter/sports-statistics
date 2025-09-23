namespace SportsStatistics.Web.Admin.Competitions;

public class CompetitionFormModel
{
    public string Name { get; set; } = string.Empty;

    public CompetitionTypeOptionDto? CompetitionType { get; set; }
}
