namespace SportsStatistics.Web.Pages.Admin.Competitions;

public sealed record CompetitionDto(Guid Id,
                                    Guid SeasonId,
                                    string Name,
                                    int FormatId,
                                    string Format);
