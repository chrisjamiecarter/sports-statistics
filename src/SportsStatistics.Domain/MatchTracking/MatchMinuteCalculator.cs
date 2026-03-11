namespace SportsStatistics.Domain.MatchTracking;

public static class MatchMinuteCalculator
{
    public static Minute Calculate(
        DateTime occuredAtUtc,
        DateTime firstHalfStartedAtUtc,
        DateTime? secondHalfStartedAtUtc)
    {
        if (secondHalfStartedAtUtc is null || occuredAtUtc < secondHalfStartedAtUtc.Value)
        {
            return Calculate(occuredAtUtc - firstHalfStartedAtUtc, MatchPeriod.FirstHalf);
        }
        else
        {
            return Calculate(occuredAtUtc - secondHalfStartedAtUtc.Value, MatchPeriod.SecondHalf);
        }
    }

    private static Minute Calculate(
        TimeSpan elapsed,
        MatchPeriod matchPeriod)
    {
        if (matchPeriod == MatchPeriod.FirstHalf)
        {
            return Calculate(elapsed, Minute.FirstHalfEndMinute);
        }
        else if (matchPeriod == MatchPeriod.SecondHalf)
        {
            return Calculate(elapsed, Minute.SecondHalfEndMinute);
        }
        else
        {
            throw new InvalidOperationException($"Cannot calculate minute for match period {matchPeriod}");
        }
    }
    private static Minute Calculate(
        TimeSpan elapsed,
        int baseEndMinute)
    {
        var elapsedMinutes = (int)elapsed.TotalMinutes + 1;
        if (elapsedMinutes <= baseEndMinute)
        {
            return Minute.Create(elapsedMinutes).Value;
        }
        else
        {
            var stoppageMinutes = elapsedMinutes - baseEndMinute;
            return Minute.Create(baseEndMinute, stoppageMinutes).Value;
        }
    }
}
