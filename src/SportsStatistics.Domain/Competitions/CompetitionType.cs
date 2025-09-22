using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed class CompetitionType : Enumeration
{
    public static readonly CompetitionType Unknown = new(0, nameof(Unknown));
    public static readonly CompetitionType Friendly = new(1, nameof(Friendly));
    public static readonly CompetitionType League = new(2, nameof(League));
    public static readonly CompetitionType Cup = new(3, nameof(Cup));
    public static readonly CompetitionType Tournament = new(4, nameof(Tournament));

    private CompetitionType(int id, string name) : base(id, name) { }

    public static IReadOnlyCollection<CompetitionType> All =>
    [
        Friendly,
        League,
        Cup,
        Tournament,
    ];

    public static CompetitionType FromName(string competitionName)
        => All.SingleOrDefault(p => string.Equals(p.Name, competitionName, StringComparison.OrdinalIgnoreCase))
        ?? Unknown;
}
