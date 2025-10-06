namespace SportsStatistics.Application.Fixtures.GetAll;

public sealed record FixtureResponse(Guid Id,
                                     Guid CompetitionId,
                                     DateTime KickoffTimeUtc,
                                     string LocationName,
                                     string StatusName);
