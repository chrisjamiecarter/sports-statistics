using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class Fixture : Entity, ISoftDeletableEntity
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

    public DateTime? DeletedOnUtc { get; private set; }

    public bool Deleted { get; private set; }

    internal static Fixture Create(Competition competition, Opponent opponent, KickoffTimeUtc kickoffTimeUtc, Location location)
    {
        var fixture = new Fixture(competition.Id, opponent, kickoffTimeUtc, location, Status.Scheduled);

        fixture.Raise(new FixtureCreatedDomainEvent(fixture.Id));

        return fixture;
    }

    public bool ChangeOpponent(Opponent opponent)
    {
        if (Opponent == opponent)
        {
            return false;
        }

        var previousOpponent = Opponent;
        Opponent = opponent;
        Raise(new FixtureOpponentChangedDomainEvent(this, previousOpponent));

        return true;
    }

    public bool ChangeKickoffTimeUtc(KickoffTimeUtc kickoffTimeUtc)
    {
        if (KickoffTimeUtc == kickoffTimeUtc)
        {
            return false;
        }

        var previousKickoffTimeUtc = KickoffTimeUtc;
        KickoffTimeUtc = kickoffTimeUtc;
        Raise(new FixtureKickoffTimeUtcChangedDomainEvent(this, previousKickoffTimeUtc));

        return true;
    }

    public bool ChangeLocation(Location location)
    {
        if (Location == location)
        {
            return false;
        }

        var previousLocation = Location;
        Location = location;
        Raise(new FixtureLocationChangedDomainEvent(this, previousLocation));

        return true;
    }

    public void Delete(DateTime utcNow)
    {
        if (Deleted)
        {
            return;
        }

        Deleted = true;
        DeletedOnUtc = utcNow;
        Raise(new FixtureDeletedDomainEvent(Id));
    }
}
