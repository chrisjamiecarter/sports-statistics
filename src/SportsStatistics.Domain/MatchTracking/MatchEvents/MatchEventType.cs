using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.MatchEvents;

public class MatchEventType : Enumeration
{
    public static readonly MatchEventType Unknown = new(0, nameof(Unknown));
    public static readonly MatchEventType FirstHalfStarted = new(1, nameof(FirstHalfStarted));
    public static readonly MatchEventType FirstHalfFinished = new(2, nameof(FirstHalfFinished));
    public static readonly MatchEventType SecondHalfStarted = new(3, nameof(SecondHalfStarted));
    public static readonly MatchEventType SecondHalfFinished = new(4, nameof(SecondHalfFinished));
    public static readonly MatchEventType HomeGoal = new(5, nameof(HomeGoal));
    public static readonly MatchEventType AwayGoal = new(6, nameof(AwayGoal));

    private MatchEventType(int id, string name) : base(id, name) { }

    public static IReadOnlyCollection<MatchEventType> All =>
    [
        FirstHalfStarted,
        FirstHalfFinished,
        SecondHalfStarted,
        SecondHalfFinished,
        HomeGoal,
        AwayGoal
    ];

    public static int MaxLength => All.Max(type => type.Name.Length);

    public static Result<MatchEventType> Create(string value)
    {
        var resolvedValue =
            All.SingleOrDefault(type => string.Equals(type.Name, value, StringComparison.OrdinalIgnoreCase))
            ?? Unknown;

        if (resolvedValue == Unknown)
        {
            return MatchTrackingErrors.MatchEventType.Unknown;
        }

        return resolvedValue;
    }
}
