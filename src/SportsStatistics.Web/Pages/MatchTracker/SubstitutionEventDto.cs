namespace SportsStatistics.Web.Pages.MatchTracker;

public sealed record SubstitutionEventDto(
    Guid FixtureId,
    Guid PlayerOffId,
    Guid PlayerOnId,
    string DisplayText,
    DateTime OccurredAtUtc);
