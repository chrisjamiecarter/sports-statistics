using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class FixtureStatus : Enumeration
{
    public static readonly FixtureStatus Unknown = new(0, nameof(Unknown));
    public static readonly FixtureStatus Scheduled = new(1, nameof(Scheduled));
    public static readonly FixtureStatus Completed = new(2, nameof(Completed));
    public static readonly FixtureStatus Postponed = new(3, nameof(Postponed));
    public static readonly FixtureStatus Cancelled = new(3, nameof(Cancelled));

    private FixtureStatus(int id, string name) : base(id, name) { }

    public static IReadOnlyCollection<FixtureStatus> All =>
    [
        Scheduled,
        Completed,
        Postponed,
        Cancelled,
    ];

    public static int MaxLength => All.Max(type => type.Name.Length);

    public static FixtureStatus FromName(string status)
    {
        return All.SingleOrDefault(s => string.Equals(s.Name, status, StringComparison.OrdinalIgnoreCase))
               ?? Unknown;
    }
}
