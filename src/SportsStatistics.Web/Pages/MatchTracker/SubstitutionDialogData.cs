namespace SportsStatistics.Web.Pages.MatchTracker;

public sealed record SubstitutionDialogData(
    IEnumerable<PlayerOptionDto> PlayerOptions,
    PlayerDto? PlayerOff,
    Guid? PlayerOnId,
    DateTime OccurredAtUtc);
