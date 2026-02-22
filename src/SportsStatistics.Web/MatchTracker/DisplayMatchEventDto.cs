namespace SportsStatistics.Web.MatchTracker;

public sealed record DisplayMatchEventDto(
    string DisplayMinute,
    string EventType,
    string PlayerName,
    DateTime OccurredAtUtc);
