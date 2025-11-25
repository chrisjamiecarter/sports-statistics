using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.MatchEvents;

public class MatchEventType : Enumeration<MatchEventType>
{
    public static readonly MatchEventType FirstHalfStarted = new(0, nameof(FirstHalfStarted));
    public static readonly MatchEventType FirstHalfFinished = new(1, nameof(FirstHalfFinished));
    public static readonly MatchEventType SecondHalfStarted = new(2, nameof(SecondHalfStarted));
    public static readonly MatchEventType SecondHalfFinished = new(3, nameof(SecondHalfFinished));
    public static readonly MatchEventType HomeGoal = new(4, nameof(HomeGoal));
    public static readonly MatchEventType AwayGoal = new(5, nameof(AwayGoal));

    private MatchEventType(int id, string name) : base(id, name) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MatchEventType"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private MatchEventType() { }
}
