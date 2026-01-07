namespace SportsStatistics.Web.Admin.Fixtures;

public record SeasonUploadModel(DateTime StartDate,
                                    DateTime EndDate,
                                    IReadOnlyList<CompetitionUploadModel> Competitions);
