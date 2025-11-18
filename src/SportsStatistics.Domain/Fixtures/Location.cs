using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class Location : Enumeration
{
    private static readonly Location Unknown = new(0, nameof(Unknown));
    private static readonly Location Home = new(1, nameof(Home));
    private static readonly Location Away = new(2, nameof(Away));
    private static readonly Location Neutral = new(3, nameof(Neutral));

    private Location(int id, string name) : base(id, name) { }

    public static IReadOnlyCollection<Location> All =>
    [
        Home,
        Away,
        Neutral,
    ];

    public static int MaxLength => All.Max(type => type.Name.Length);

    public static Result<Location> Create(string value)
    {
        var parsedLocation =
            All.SingleOrDefault(location => string.Equals(location.Name, value, StringComparison.OrdinalIgnoreCase))
            ?? Unknown;

        if (parsedLocation == Unknown)
        {
            return FixtureErrors.Location.Unknown;
        }

        return parsedLocation;
    }
}
