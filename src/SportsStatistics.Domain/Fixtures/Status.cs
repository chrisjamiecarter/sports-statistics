using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class Status : Enumeration
{
    public static readonly Status Unknown = new(0, nameof(Unknown));
    public static readonly Status Scheduled = new(1, nameof(Scheduled));
    public static readonly Status Completed = new(2, nameof(Completed));
    public static readonly Status Postponed = new(3, nameof(Postponed));
    public static readonly Status Cancelled = new(3, nameof(Cancelled));

    private Status(int id, string name) : base(id, name) { }

    public static IReadOnlyCollection<Status> All =>
    [
        Scheduled,
        Completed,
        Postponed,
        Cancelled,
    ];

    public static int MaxLength => All.Max(type => type.Name.Length);

    public static Result<Status> Create(string value)
    {
        var parsedStatus =
            All.SingleOrDefault(status => string.Equals(status.Name, value, StringComparison.OrdinalIgnoreCase))
            ?? Unknown;

        if (parsedStatus == Unknown)
        {
            return FixtureErrors.Status.Unknown;
        }

        return parsedStatus;
    }
}
