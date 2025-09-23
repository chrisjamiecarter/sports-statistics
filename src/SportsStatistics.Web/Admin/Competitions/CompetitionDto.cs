namespace SportsStatistics.Web.Admin.Competitions;

public sealed record CompetitionDto(Guid Id,
                                    string Name,
                                    string CompetitionType);
