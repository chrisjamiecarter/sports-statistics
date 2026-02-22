namespace SportsStatistics.Web.MatchTracker;

public sealed record SubstitutionEventDto(
    Guid FixtureId,
    Guid PlayerOffId,
    Guid PlayerOnId,
    int BaseMinute,
    int? StoppageMinute,
    string DisplayText,
    DateTime OccurredAtUtc);
