namespace SportsStatistics.Web.MatchTracker;

public sealed record DisplayMatchEventDto(
    int Minute,
    string EventType,
    string PlayerName,
    DateTime OccurredAtUtc);
