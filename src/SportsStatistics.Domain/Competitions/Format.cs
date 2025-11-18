using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed class Format : Enumeration
{
    private static readonly Format Unknown = new(0, nameof(Unknown));
    private static readonly Format League = new(1, nameof(League));
    private static readonly Format Cup = new(2, nameof(Cup));

    private Format(int id, string name) : base(id, name) { }

    public static IReadOnlyCollection<Format> All =>
    [
        League,
        Cup,
    ];

    public static int MaxLength => All.Max(type => type.Name.Length);

    public static Result<Format> Create(string formatName)
    {
        var parsedFormat = 
            All.SingleOrDefault(format => string.Equals(format.Name, formatName, StringComparison.OrdinalIgnoreCase)) 
            ?? Unknown;

        if (parsedFormat == Unknown)
        {
            return CompetitionErrors.Format.Unknown;
        }

        return parsedFormat;
    }
}
