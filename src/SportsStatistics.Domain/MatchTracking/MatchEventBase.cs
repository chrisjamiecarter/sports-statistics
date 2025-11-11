using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.MatchEvents;

public abstract class MatchEventBase : Entity
{
    protected MatchEventBase(EntityId id, EntityId fixtureId, int minute, DateTime occurredAtUtc) : base(id)
    {
        FixtureId = fixtureId;
        Minute = minute;
        OccurredAtUtc = occurredAtUtc;
    }

    public EntityId FixtureId { get; private set; }

    public int Minute { get; private set; }

    public DateTime OccurredAtUtc { get; private set; }
}
