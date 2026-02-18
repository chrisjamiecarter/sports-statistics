namespace SportsStatistics.Web.MatchTracker;

public sealed record SubstitutionEventDto(Guid FixtureId,
                                          Guid PlayerOffId,
                                          Guid PlayerOnId,
                                          int Minute,
                                          string DisplayText,
                                          DateTime OccurredAtUtc);
