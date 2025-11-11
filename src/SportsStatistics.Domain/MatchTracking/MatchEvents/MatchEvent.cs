using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.MatchEvents;

public sealed class MatchEvent : MatchEventBase
{
    private MatchEvent(EntityId id, EntityId fixtureId, MatchEventType matchEventType, int minute, DateTime occurredAtUtc) : base(id, fixtureId, minute, occurredAtUtc)
    {
        Type = matchEventType;
    }

    public MatchEventType Type { get; private set; } = MatchEventType.Unknown;

    public static MatchEvent Create(EntityId fixtureId, string matchEventTypeName, int minute, DateTime occurredAtUtc)
    {
        var matchEventType = MatchEventType.FromName(matchEventTypeName);

        return new(EntityId.Create(), fixtureId, matchEventType, minute, occurredAtUtc);
    }
}
