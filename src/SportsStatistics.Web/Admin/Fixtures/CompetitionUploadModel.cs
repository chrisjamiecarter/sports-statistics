namespace SportsStatistics.Web.Admin.Fixtures;

public record CompetitionUploadModel(string Name,
                                     string Format,
                                     IReadOnlyList<FixtureUploadModel> Fixtures);
