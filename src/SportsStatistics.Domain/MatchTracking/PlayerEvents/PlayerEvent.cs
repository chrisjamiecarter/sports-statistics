using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.PlayerEvents;

public sealed class PlayerEvent : MatchEventBase
{
    private PlayerEvent(EntityId id, EntityId fixtureId, EntityId playerId, PlayerEventType type, int minute, DateTime occurredAtUtc) : base(id, fixtureId, minute, occurredAtUtc)
    {
        PlayerId = playerId;
        Type = type;
    }

    public EntityId PlayerId { get; private set; }

    public PlayerEventType Type { get; private set; } = PlayerEventType.Unknown;

    public static PlayerEvent Create(EntityId fixtureId, EntityId playerId, string playerEventTypeName, int minute, DateTime occurredAtUtc)
    {
        var playerEventType = PlayerEventType.FromName(playerEventTypeName);

        return new(EntityId.Create(), fixtureId, playerId, playerEventType, minute, occurredAtUtc);
    }
}
