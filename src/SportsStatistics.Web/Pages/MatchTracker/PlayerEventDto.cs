namespace SportsStatistics.Web.Pages.MatchTracker;

public sealed record PlayerEventDto(
    Guid FixtureId,
    Guid PlayerId,
    int PlayerEventTypeId,
    string DisplayText,
    DateTime OccurredAtUtc);
