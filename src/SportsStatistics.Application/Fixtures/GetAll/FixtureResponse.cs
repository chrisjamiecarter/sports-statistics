namespace SportsStatistics.Application.Fixtures.GetAll;

public sealed record FixtureResponse(Guid Id,
                                     DateTime KickoffTimeUtc,
                                     Guid CompetitionId,
                                     string CompetitionName,
                                     string CompetitionType,
                                     string LocationName,
                                     string StatusName);
