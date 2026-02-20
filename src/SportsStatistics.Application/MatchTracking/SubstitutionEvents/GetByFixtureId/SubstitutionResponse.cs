namespace SportsStatistics.Application.MatchTracking.SubstitutionEvents.GetByFixtureId;

public sealed record SubstitutionResponse(Guid Id,
                                          Guid PlayerOffId,
                                          Guid PlayerOnId,
                                          int Minute,
                                          DateTime OccurredAtUtc);
