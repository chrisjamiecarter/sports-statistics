using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.SubstitutionEvents;

public class SubstitutionEvent : MatchEventBase
{
    private SubstitutionEvent(EntityId id, EntityId fixtureId, EntityId playerOutId, EntityId playerInId, int minute, DateTime occurredAtUtc) : base(id, fixtureId, minute, occurredAtUtc)
    {
        PlayerOutId = playerOutId;
        PlayerInId = playerInId;
    }

    public EntityId PlayerOutId { get; private set; }

    public EntityId PlayerInId { get; private set; }

    public static SubstitutionEvent Create(EntityId fixtureId, EntityId playerOutId, EntityId playerInId, int minute, DateTime occurredAtUtc)
    {
        return new SubstitutionEvent(EntityId.Create(), fixtureId, playerOutId, playerInId, minute, occurredAtUtc);
    }
}
