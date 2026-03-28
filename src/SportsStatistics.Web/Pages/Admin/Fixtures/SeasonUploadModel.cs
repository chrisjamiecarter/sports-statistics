namespace SportsStatistics.Web.Pages.Admin.Fixtures;

public record SeasonUploadModel(DateTime StartDate,
                                DateTime EndDate,
                                IReadOnlyList<CompetitionUploadModel> Competitions);
