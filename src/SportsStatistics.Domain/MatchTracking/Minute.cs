using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking;

public sealed record Minute
{
    public const int MinValue = 0;

    private Minute(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static implicit operator int(Minute? minute) =>
        minute is not null ? minute.Value : throw new ArgumentNullException(nameof(minute));

    public static Result<Minute> Create(int? value)
    {
        if (value is null)
        {
            return MatchEventBaseErrors.Minute.NullOrEmpty;
        }

        if (value < MinValue)
        {
            return MatchEventBaseErrors.Minute.BelowMinValue;
        }

        return new Minute(value.Value);
    }
}
