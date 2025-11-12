using SportsStatistics.Domain.MatchTracking.MatchEvents;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class MatchEventTypeConverter : ValueObjectConverter<MatchEventType>
{
    public static readonly MatchEventTypeConverter Instance = new();

    private MatchEventTypeConverter() : base(type => type.Name, value => MatchEventType.FromName(value)) { }
}
