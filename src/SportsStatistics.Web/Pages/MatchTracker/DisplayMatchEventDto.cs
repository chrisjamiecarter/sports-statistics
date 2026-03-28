namespace SportsStatistics.Web.Pages.MatchTracker;

public sealed record DisplayMatchEventDto(
    string MinuteDisplay,
    string EventTypeDisplay,
    DateTime OccurredAtUtc);
