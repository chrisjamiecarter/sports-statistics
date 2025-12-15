using SportsStatistics.Domain.MatchTracking;

namespace SportsStatistics.Domain.Tests.MatchTracking.TestData;

public static class MinuteTestData
{
    public static Minute ValidMinute => Minute.Create(Minute.MinValue).Value;

    public static readonly int BelowMinValueMinute = Minute.MinValue - 1;

    public static readonly int? NullMinute;
}
