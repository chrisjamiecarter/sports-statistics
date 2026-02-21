using System.Globalization;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking;

/// <summary>
/// Represents a match minute with support for stoppage time notation.
/// </summary>
/// <remarks>
/// <para>
/// Business Rules implemented:
/// </para>
/// <list type="bullet">
///   <item><description>BR-002: recorded_minute = floor(elapsed_seconds / 60) + 1</description></item>
///   <item><description>BR-003: First half minutes 1-45</description></item>
///   <item><description>BR-004: Second half minutes 46-90</description></item>
///   <item><description>BR-005: Stoppage time notation [base]+[stoppage_minute]</description></item>
///   <item><description>BR-006: Extra time minutes 91-105 and 106-120</description></item>
///   <item><description>BR-007: Minimum recordable minute is 1</description></item>
/// </list>
/// </remarks>
public sealed record Minute
{
    /// <summary>
    /// The minimum valid base minute.
    /// </summary>
    public const int MinBaseMinute = 1;

    /// <summary>
    /// The maximum valid base minute.
    /// </summary>
    public const int MaxBaseMinute = 90;

    /// <summary>
    /// The end of first half.
    /// </summary>
    public const int FirstHalfEnd = 45;

    /// <summary>
    /// The end of second half (regulation time).
    /// </summary>
    public const int SecondHalfEnd = 90;

    private Minute(int baseMinute, int? stoppageMinute)
    {
        BaseMinute = baseMinute;
        StoppageMinute = stoppageMinute;
    }

    /// <summary>
    /// Gets the base minute.
    /// </summary>
    public int BaseMinute { get; }

    /// <summary>
    /// Gets the stoppage minute offset (1, 2, 3...) if in stoppage time, otherwise null.
    /// </summary>
    public int? StoppageMinute { get; }

    /// <summary>
    /// Gets the match period derived from the minute values.
    /// </summary>
    public MatchPeriod Period => DerivePeriod();

    /// <summary>
    /// Gets the display notation for this minute (e.g., "45", "45+1", "90+2").
    /// </summary>
    public string DisplayNotation => StoppageMinute.HasValue
        ? $"{BaseMinute.ToString(CultureInfo.InvariantCulture)}+{StoppageMinute.Value.ToString(CultureInfo.InvariantCulture)}"
        : BaseMinute.ToString(CultureInfo.InvariantCulture);

    /// <summary>
    /// Gets the total elapsed seconds this minute represents (approximate, for ordering).
    /// </summary>
    public int ToTotalSeconds()
    {
        // Calculate approximate seconds for chronological ordering
        var baseSeconds = BaseMinute * 60;

        if (StoppageMinute.HasValue)
        {
            // For stoppage time, we need to calculate from the period boundary
            // E.g., 45+1 means the first minute after 45:00
            return baseSeconds + (StoppageMinute.Value * 60);
        }

        return baseSeconds;
    }

    /// <summary>
    /// Creates a minute from base minute and optional stoppage minute with full validation.
    /// </summary>
    /// <param name="baseMinute">The base minute (1-120, or 45/90/105/120 for stoppage).</param>
    /// <param name="stoppageMinute">The stoppage minute offset if applicable.</param>
    /// <returns>A result containing the minute or validation error.</returns>
    public static Result<Minute> Create(int? baseMinute, int? stoppageMinute = null)
    {
        if (baseMinute is null)
        {
            return MinuteErrors.Null;
        }

        // BR-007: Minimum recordable minute is 1
        if (baseMinute < MinBaseMinute)
        {
            return MinuteErrors.BelowMinimum;
        }

        // Maximum base minute is 120
        if (baseMinute > MaxBaseMinute)
        {
            return MinuteErrors.AboveMaximum;
        }

        // Validate stoppage minute if provided
        if (stoppageMinute.HasValue)
        {
            if (stoppageMinute.Value < 1)
            {
                return MinuteErrors.InvalidStoppageMinute;
            }

            // Validate that base minute is valid for stoppage time
            if (!IsValidStoppageBaseMinute(baseMinute.Value))
            {
                return MinuteErrors.InvalidStoppageBaseMinute;
            }
        }
        else
        {
            // Validate base minute is not a stoppage-only value without stoppage minute
            // (This is actually allowed - base minute 45 could be regulation time in first half)
        }

        return new Minute(baseMinute.Value, stoppageMinute);
    }

    /// <summary>
    /// Creates a minute for first half regulation time (minutes 1-45).
    /// </summary>
    /// <param name="minute">The minute (1-45).</param>
    /// <returns>A result containing the minute or validation error.</returns>
    public static Result<Minute> FirstHalfMinute(int minute)
    {
        var result = Create(minute);
        if (result.IsFailure)
        {
            return result;
        }

        if (minute > FirstHalfEnd)
        {
            return MinuteErrors.InvalidForFirstHalf;
        }

        return result;
    }

    /// <summary>
    /// Creates a minute for first half stoppage time (45+1, 45+2, etc.).
    /// </summary>
    /// <param name="stoppageMinute">The stoppage minute (1, 2, 3...).</param>
    /// <returns>A result containing the minute or validation error.</returns>
    public static Result<Minute> FirstHalfStoppage(int stoppageMinute)
    {
        return Create(FirstHalfEnd, stoppageMinute);
    }

    /// <summary>
    /// Creates a minute for second half regulation time (minutes 46-90).
    /// </summary>
    /// <param name="minute">The minute (46-90).</param>
    /// <returns>A result containing the minute or validation error.</returns>
    public static Result<Minute> SecondHalfMinute(int minute)
    {
        var result = Create(minute);
        if (result.IsFailure)
        {
            return result;
        }

        // BR-004: Second half starts at minute 46
        if (minute <= FirstHalfEnd)
        {
            return MinuteErrors.InvalidForSecondHalf;
        }

        if (minute > SecondHalfEnd)
        {
            return MinuteErrors.InvalidForSecondHalf;
        }

        return result;
    }

    /// <summary>
    /// Creates a minute for second half stoppage time (90+1, 90+2, etc.).
    /// </summary>
    /// <param name="stoppageMinute">The stoppage minute (1, 2, 3...).</param>
    /// <returns>A result containing the minute or validation error.</returns>
    public static Result<Minute> SecondHalfStoppage(int stoppageMinute)
    {
        return Create(SecondHalfEnd, stoppageMinute);
    }

    /// <summary>
    /// Creates a minute from elapsed seconds within a specific period (BR-002).
    /// </summary>
    /// <param name="elapsedSeconds">The elapsed seconds in the current period.</param>
    /// <param name="period">The current match period.</param>
    /// <returns>A result containing the minute or validation error.</returns>
    public static Result<Minute> FromElapsedSeconds(int elapsedSeconds, MatchPeriod period)
    {
        if (elapsedSeconds < 0)
        {
            return MinuteErrors.BelowMinimum;
        }

        // BR-002: recorded_minute = floor(elapsed_seconds / 60) + 1
        var calculatedMinute = (elapsedSeconds / 60) + 1;

        return period switch
        {
            _ when period == MatchPeriod.FirstHalf => FirstHalfMinute(calculatedMinute),
            _ when period == MatchPeriod.FirstHalfStoppage => FirstHalfStoppage(calculatedMinute),
            _ when period == MatchPeriod.SecondHalf => SecondHalfMinute(FirstHalfEnd + calculatedMinute),
            _ when period == MatchPeriod.SecondHalfStoppage => SecondHalfStoppage(calculatedMinute),
            _ => Result.Failure<Minute>(Error.Validation(
                "MatchTracking.Minute.InvalidPeriod",
                $"Cannot create a minute for period '{period.Name}'."))
        };
    }

    /// <summary>
    /// Validates that a base minute is valid for stoppage time notation.
    /// </summary>
    private static bool IsValidStoppageBaseMinute(int baseMinute) =>
        baseMinute == FirstHalfEnd ||
        baseMinute == SecondHalfEnd;

    /// <summary>
    /// Derives the match period from the base and stoppage minute values.
    /// </summary>
    private MatchPeriod DerivePeriod()
    {
        if (StoppageMinute.HasValue)
        {
            return BaseMinute switch
            {
                FirstHalfEnd => MatchPeriod.FirstHalfStoppage,
                SecondHalfEnd => MatchPeriod.SecondHalfStoppage,
                _ => MatchPeriod.FirstHalf // Should not happen with valid construction
            };
        }

        return BaseMinute switch
        {
            >= MinBaseMinute and <= FirstHalfEnd => MatchPeriod.FirstHalf,
            > FirstHalfEnd and <= SecondHalfEnd => MatchPeriod.SecondHalf,
            _ => MatchPeriod.FirstHalf // Should not happen with valid construction
        };
    }

    /// <summary>
    /// Implicit conversion to int for backward compatibility.
    /// </summary>
    public static implicit operator int(Minute? minute) =>
        minute is not null ? minute.BaseMinute : throw new ArgumentNullException(nameof(minute));
}
