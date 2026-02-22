namespace SportsStatistics.Application.MatchTracking.GetByFixtureId;

/// <summary>
/// Response model for match event data.
/// </summary>
/// <param name="Id">The unique identifier for the match event.</param>
/// <param name="FixtureId">The fixture identifier.</param>
/// <param name="EventType">The name of the event type.</param>
/// <param name="BaseMinute">The base minute (1-120).</param>
/// <param name="StoppageMinute">The stoppage minute offset if applicable.</param>
/// <param name="DisplayMinute">The display notation (e.g., "45", "45+1", "90+2").</param>
/// <param name="Period">The match period name.</param>
/// <param name="OccurredAtUtc">The UTC timestamp when the event occurred.</param>
/// <param name="PlayerName">The player name if this is a player event.</param>
/// <param name="PlayerId">The player identifier if this is a player event.</param>
public sealed record MatchEventResponse(Guid Id,
                                        Guid FixtureId,
                                        string EventType,
                                        int BaseMinute,
                                        int? StoppageMinute,
                                        string DisplayMinute,
                                        string Period,
                                        DateTime OccurredAtUtc,
                                        string? PlayerName,
                                        Guid? PlayerId);
