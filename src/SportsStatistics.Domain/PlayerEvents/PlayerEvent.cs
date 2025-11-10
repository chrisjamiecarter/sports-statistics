using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.PlayerEvents;

public sealed class PlayerEvent : Entity
{
    public PlayerEvent(EntityId id, EntityId fixtureId, EntityId playerId, PlayerEventType playerEventType, int minute, DateTime occurredAtUtc) : base(id)
    {
        FixtureId = fixtureId;
        PlayerId = playerId;
        Type = playerEventType;
        Minute = minute;
        OccurredAtUtc = occurredAtUtc;
    }

    public EntityId FixtureId { get; private set; }

    public EntityId PlayerId { get; private set; }

    public PlayerEventType Type { get; private set; } = PlayerEventType.Unknown;

    public int Minute { get; private set; }

    public DateTime OccurredAtUtc { get; private set; }

    public static PlayerEvent Create(EntityId fixtureId, EntityId playerId, string playerEventTypeName, int minute, DateTime occurredAtUtc)
    {
        var playerEventType = PlayerEventType.FromName(playerEventTypeName);

        return new(EntityId.Create(), fixtureId, playerId, playerEventType, minute, occurredAtUtc);
    }
}
