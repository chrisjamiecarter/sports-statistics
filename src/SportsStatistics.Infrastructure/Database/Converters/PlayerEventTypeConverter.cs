using SportsStatistics.Domain.MatchTracking.PlayerEvents;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class PlayerEventTypeConverter : ValueObjectConverter<PlayerEventType>
{
    public static readonly PlayerEventTypeConverter Instance = new();

    private PlayerEventTypeConverter() : base(type => type.Name, value => PlayerEventType.FromName(value)) { }
}
