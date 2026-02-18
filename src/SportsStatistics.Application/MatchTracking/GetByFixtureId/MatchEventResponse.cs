namespace SportsStatistics.Application.MatchTracking.GetByFixtureId;

public sealed record MatchEventResponse(Guid Id,
                                        Guid FixtureId,
                                        string EventType,
                                        int Minute,
                                        DateTime OccurredAtUtc,
                                        string? PlayerName,
                                        Guid? PlayerId);
