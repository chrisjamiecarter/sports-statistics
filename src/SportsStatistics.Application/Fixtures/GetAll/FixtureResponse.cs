namespace SportsStatistics.Application.Fixtures.GetAll;

public sealed record FixtureResponse(Guid Id,
                                     Guid CompetitionId,
                                     string CompetitionName,
                                     string Opponent,
                                     DateTime KickoffTimeUtc,
                                     string LocationName,
                                     int HomeGoals,
                                     int AwayGoals,
                                     string StatusName);
