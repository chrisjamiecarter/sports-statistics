using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking;

/// <summary>
/// Represents the periods of a football match.
/// </summary>
public sealed class MatchPeriod : Enumeration<MatchPeriod>
{
    /// <summary>
    /// First half of regulation time (minutes 1-45).
    /// </summary>
    public static readonly MatchPeriod FirstHalf = new(0, nameof(FirstHalf));

    /// <summary>
    /// Stoppage time at the end of the first half (minutes 45+1, 45+2, etc.).
    /// </summary>
    public static readonly MatchPeriod FirstHalfStoppage = new(1, nameof(FirstHalfStoppage));

    /// <summary>
    /// Half-time interval between first and second half.
    /// </summary>
    public static readonly MatchPeriod HalfTime = new(2, nameof(HalfTime));

    /// <summary>
    /// Second half of regulation time (minutes 46-90).
    /// </summary>
    public static readonly MatchPeriod SecondHalf = new(3, nameof(SecondHalf));

    /// <summary>
    /// Stoppage time at the end of the second half (minutes 90+1, 90+2, etc.).
    /// </summary>
    public static readonly MatchPeriod SecondHalfStoppage = new(4, nameof(SecondHalfStoppage));

    /// <summary>
    /// Full-time (end of regulation, if no extra time required).
    /// </summary>
    public static readonly MatchPeriod FullTime = new(5, nameof(FullTime));

    private MatchPeriod(int id, string name) : base(id, name) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MatchPeriod"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private MatchPeriod() { }

    /// <summary>
    /// Gets the base minute for this period (the minute displayed before any stoppage notation).
    /// </summary>
    /// <returns>The base minute (45, 90, 105, or 120) for stoppage periods; otherwise null.</returns>
    public int? GetStoppageBaseMinute() => this switch
    {
        _ when this == FirstHalfStoppage => 45,
        _ when this == SecondHalfStoppage => 90,
        _ => null
    };

    /// <summary>
    /// Determines whether this period is a stoppage time period.
    /// </summary>
    public bool IsStoppageTime() => this == FirstHalfStoppage ||
                                    this == SecondHalfStoppage;

    /// <summary>
    /// Determines whether this period allows match events to be recorded.
    /// </summary>
    public bool IsPlayingPeriod() => this == FirstHalf ||
                                     this == FirstHalfStoppage ||
                                     this == SecondHalf ||
                                     this == SecondHalfStoppage;
}
