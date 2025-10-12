using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class Fixture : Entity
{
    private Fixture(EntityId id, EntityId competitionId, string opponent, DateTime kickoffTimeUtc, FixtureLocation location, FixtureStatus status) : base(id)
    {
        CompetitionId = competitionId;
        Opponent = opponent;
        KickoffTimeUtc = kickoffTimeUtc;
        Location = location;
        Score = FixtureScore.Create(0, 0);
        Status = status;
    }

    public EntityId CompetitionId { get; private set; }

    public string Opponent { get; private set; } = string.Empty;

    public DateTime KickoffTimeUtc { get; private set; }

    public FixtureLocation Location { get; private set; } = FixtureLocation.Unknown;

    public FixtureScore Score { get; private set; }

    public FixtureStatus Status { get; private set; } = FixtureStatus.Unknown;

    public static Fixture Create(EntityId competitionId, string opponent, DateTime kickoffTimeUtc, FixtureLocation location)
    {
        ValidateAndThrow(opponent, location);

        return new Fixture(EntityId.Create(), competitionId, opponent, kickoffTimeUtc, location, FixtureStatus.Scheduled);
    }

    public void Update(string opponent, DateTime kickoffTimeUtc, FixtureLocation location)
    {
        ValidateAndThrow(opponent, location);

        KickoffTimeUtc = kickoffTimeUtc;
        Location = location;
    }

    private static void ValidateAndThrow(string opponent, FixtureLocation location)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(opponent, nameof(opponent));
        ArgumentNullException.ThrowIfNull(location, nameof(location));
        //ArgumentNullException.ThrowIfNull(status, nameof(status));

        if (location == FixtureLocation.Unknown)
        {
            throw new ArgumentException("A fixture cannot have a location of unknown.", nameof(location));
        }

        //if (status == FixtureStatus.Unknown)
        //{
        //    throw new ArgumentException("A fixture cannot have a status of unknown.", nameof(status));
        //}
    }
}
