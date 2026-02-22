namespace SportsStatistics.Web.MatchTracker;

public sealed record PlayerEventDto(
    Guid FixtureId,
    Guid PlayerId,
    int PlayerEventTypeId,
    int BaseMinute,
    int? StoppageMinute,
    string DisplayText,
    DateTime OccurredAtUtc);
