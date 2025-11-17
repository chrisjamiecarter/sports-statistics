using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class FixtureLocation : Enumeration
{
    public static readonly FixtureLocation Unknown = new(0, nameof(Unknown));
    public static readonly FixtureLocation Home = new(1, nameof(Home));
    public static readonly FixtureLocation Away = new(2, nameof(Away));
    public static readonly FixtureLocation Neutral = new(3, nameof(Neutral));

    private FixtureLocation(int id, string name) : base(id, name) { }

    public static IReadOnlyCollection<FixtureLocation> All =>
    [
        Home,
        Away,
        Neutral,
    ];

    public static int MaxLength => All.Max(type => type.Name.Length);

    public static FixtureLocation FromName(string location)
    {
        return All.SingleOrDefault(l => string.Equals(l.Name, location, StringComparison.OrdinalIgnoreCase))
               ?? Unknown;
    }
}
