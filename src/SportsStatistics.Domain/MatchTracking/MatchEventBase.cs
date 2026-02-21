using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking;

public abstract class MatchEventBase : Entity, ISoftDeletableEntity
{
    protected MatchEventBase(Guid fixtureId,
                             Minute minute,
                             DateTime occurredAtUtc)
        : base(Guid.CreateVersion7())
    {
        FixtureId = fixtureId;
        Minute = minute;
        Period = minute.Period;
        OccurredAtUtc = occurredAtUtc;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="MatchEventBase"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    protected MatchEventBase() { }

    public Guid FixtureId { get; private set; } = default!;

    public Minute Minute { get; private set; } = default!;

    /// <summary>
    /// Gets the match period derived from the minute value.
    /// </summary>
    public MatchPeriod Period { get; private set; } = default!;

    public DateTime OccurredAtUtc { get; private set; } = default!;

    public DateTime? DeletedOnUtc { get; private set; }

    public bool Deleted { get; private set; }
}
