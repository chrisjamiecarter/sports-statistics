using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed class CompetitionType : Enumeration
{
    public static readonly CompetitionType Unknown = new(0, nameof(Unknown));
    public static readonly CompetitionType League = new(1, nameof(League));
    public static readonly CompetitionType Cup = new(2, nameof(Cup));

    private CompetitionType(int id, string name) : base(id, name) { }

    public static IReadOnlyCollection<CompetitionType> All =>
    [
        League,
        Cup,
    ];

    public static CompetitionType FromName(string competitionName)
        => All.SingleOrDefault(p => string.Equals(p.Name, competitionName, StringComparison.OrdinalIgnoreCase))
        ?? Unknown;
}
