namespace SportsStatistics.Infrastructure.Database.Converters;

internal static class Converters
{
    public static readonly EntityIdConverter EntityIdConverter = EntityIdConverter.Instance;
    public static readonly MatchEventTypeConverter MatchEventTypeConverter = MatchEventTypeConverter.Instance;
}
