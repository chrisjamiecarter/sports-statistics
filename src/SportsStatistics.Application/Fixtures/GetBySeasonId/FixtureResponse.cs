namespace SportsStatistics.Application.Fixtures.GetBySeasonId;

public sealed record FixtureResponse(Guid Id,
                                     Guid CompetitionId,
                                     string CompetitionName,
                                     string Opponent,
                                     DateTime KickoffTimeUtc,
                                     string LocationName,
                                     int HomeGoals,
                                     int AwayGoals,
                                     string StatusName);
