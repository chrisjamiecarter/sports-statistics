using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed class Format : Enumeration
{
    public static readonly Format Unknown = new(0, nameof(Unknown));
    public static readonly Format League = new(1, nameof(League));
    public static readonly Format Cup = new(2, nameof(Cup));

    private Format(int id, string name) : base(id, name) { }

    public static IReadOnlyCollection<Format> All =>
    [
        League,
        Cup,
    ];

    public static int MaxLength => All.Max(type => type.Name.Length);

    public static Result<Format> Create(string value)
    {
        var resolvedValue = 
            All.SingleOrDefault(format => string.Equals(format.Name, value, StringComparison.OrdinalIgnoreCase)) 
            ?? Unknown;

        if (resolvedValue == Unknown)
        {
            return CompetitionErrors.Format.Unknown;
        }

        return resolvedValue;
    }
}
