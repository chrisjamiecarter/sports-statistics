using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class Fixture : Entity
{
    private Fixture(Guid competitionId,
                    Opponent opponent,
                    KickoffTimeUtc kickoffTimeUtc,
                    Location location,
                    Status status)
        : base(Guid.CreateVersion7())
    {
        CompetitionId = competitionId;
        Opponent = opponent;
        KickoffTimeUtc = kickoffTimeUtc;
        Location = location;
        Score = Score.Create(0, 0).Value;
        Status = status;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Fixture"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    private Fixture() { }

    public Guid CompetitionId { get; private set; } = default!;

    public Opponent Opponent { get; private set; } = default!;

    public KickoffTimeUtc KickoffTimeUtc { get; private set; } = default!;

    public Location Location { get; private set; } = default!;

    public Score Score { get; private set; } = default!;

    public Status Status { get; private set; } = default!;

    public static Fixture Create(Guid competitionId, Opponent opponent, KickoffTimeUtc kickoffTimeUtc, Location location)
    {
        return new(competitionId, opponent, kickoffTimeUtc, location, Status.Scheduled);
    }

    public bool ChangeOpponent(Opponent opponent)
    {
        if (Opponent == opponent)
        {
            return false;
        }

        // TODO: Raise Domain Event.
        //string previousOpponent = Opponent;
        Opponent = opponent;
        //Raise(new FixtureOpponentChangedDomainEvent(this, previousOpponent));

        return true;
    }

    public bool ChangeKickoffTimeUtc(KickoffTimeUtc kickoffTimeUtc)
    {
        if (KickoffTimeUtc == kickoffTimeUtc)
        {
            return false;
        }

        // TODO: Raise Domain Event.
        //string previousKickoffTimeUtc = KickoffTimeUtc;
        KickoffTimeUtc = kickoffTimeUtc;
        //Raise(new FixtureKickoffTimeUtcChangedDomainEvent(this, previousKickoffTimeUtc));

        return true;
    }

    public bool ChangeLocation(Location location)
    {
        if (Location == location)
        {
            return false;
        }

        // TODO: Raise Domain Event.
        //string previousLocation = Location;
        Location = location;
        //Raise(new FixtureLocationChangedDomainEvent(this, previousLocation));

        return true;
    }
}
