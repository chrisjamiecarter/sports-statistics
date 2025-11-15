namespace SportsStatistics.Infrastructure.Database.Converters;

internal static class Converters
{
    public static readonly EntityIdConverter EntityIdConverter = EntityIdConverter.Instance;
    public static readonly FixtureStatusConverter FixtureStatusConverter = FixtureStatusConverter.Instance;
    public static readonly MatchEventTypeConverter MatchEventTypeConverter = MatchEventTypeConverter.Instance;
    public static readonly PlayerEventTypeConverter PlayerEventTypeConverter = PlayerEventTypeConverter.Instance;
}
