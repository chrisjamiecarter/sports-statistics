using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchEvents;

public sealed class MatchEvent : Entity
{
    public MatchEvent(EntityId id, EntityId fixtureId, MatchEventType matchEventType, int minute, DateTime occurredAtUtc) : base(id)
    {
        FixtureId = fixtureId;
        Type = matchEventType;
        Minute = minute;
        OccurredAtUtc = occurredAtUtc;
    }

    public EntityId FixtureId { get; private set; }

    public MatchEventType Type { get; private set; } = MatchEventType.Unknown;

    public int Minute { get; private set; }

    public DateTime OccurredAtUtc { get; private set; }
}
