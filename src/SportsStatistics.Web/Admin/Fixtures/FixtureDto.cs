namespace SportsStatistics.Web.Admin.Fixtures;

public sealed record FixtureDto(Guid Id,
                                DateTime KickoffTimeUtc,
                                string Location,
                                string Score);
