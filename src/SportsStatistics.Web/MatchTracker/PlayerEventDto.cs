namespace SportsStatistics.Web.MatchTracker;

public sealed record PlayerEventDto(Guid FixtureId,
                                    Guid PlayerId,
                                    int PlayerEventTypeId,
                                    int Minute,
                                    string DisplayText,
                                    DateTime OccurredAtUtc);
