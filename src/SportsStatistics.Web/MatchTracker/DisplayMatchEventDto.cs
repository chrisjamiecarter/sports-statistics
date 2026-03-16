namespace SportsStatistics.Web.MatchTracker;

public sealed record DisplayMatchEventDto(
    string MinuteDisplay,
    string EventTypeDisplay,
    DateTime OccurredAtUtc);
