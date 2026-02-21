using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class Status : Enumeration<Status>
{
    public static readonly Status Scheduled = new(0, nameof(Scheduled));
    public static readonly Status InProgress = new(1, nameof(InProgress));
    public static readonly Status Completed = new(2, nameof(Completed));
    public static readonly Status Postponed = new(3, nameof(Postponed));
    public static readonly Status Cancelled = new(4, nameof(Cancelled));

    private Status(int id, string name) : base(id, name) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Status"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private Status() { }
}
