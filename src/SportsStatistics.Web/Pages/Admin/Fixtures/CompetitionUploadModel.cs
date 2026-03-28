namespace SportsStatistics.Web.Pages.Admin.Fixtures;

public record CompetitionUploadModel(string Name,
                                     string Format,
                                     IReadOnlyList<FixtureUploadModel> Fixtures);
