using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class Fixture : Entity
{
    private Fixture(EntityId id, EntityId competitionId, DateTime kickoffTimeUtc, FixtureLocation location, FixtureScore score, FixtureStatus status) : base(id)
    {
        CompetitionId = competitionId;
        KickoffTimeUtc = kickoffTimeUtc;
        Location = location;
        Score = score;
        Status = status;
    }

    public EntityId CompetitionId { get; private set; }

    public DateTime KickoffTimeUtc { get; private set; }

    public FixtureLocation Location { get; private set; } = FixtureLocation.Unknown;

    public FixtureScore Score { get; private set; }

    public FixtureStatus Status { get; private set; } = FixtureStatus.Unknown;

    public static Fixture Create(EntityId competitionId, DateTime kickoffTimeUtc, FixtureLocation location)
    {
        var score = FixtureScore.Create(0, 0);
        var status = FixtureStatus.Scheduled;

        ValidateAndThrow(location, score, status);

        return new Fixture(EntityId.Create(), competitionId, kickoffTimeUtc, location, score, status);
    }

    public void Update(DateTime kickoffTimeUtc, FixtureLocation location, FixtureScore score, FixtureStatus status)
    {
        ValidateAndThrow(location, score, status);

        KickoffTimeUtc = kickoffTimeUtc;
        Location = location;
        Score = score;
        Status = status;
    }

    private static void ValidateAndThrow(FixtureLocation location, FixtureScore score, FixtureStatus status)
    {
        ArgumentNullException.ThrowIfNull(location);
        ArgumentNullException.ThrowIfNull(score);
        ArgumentNullException.ThrowIfNull(status);

        if (location == FixtureLocation.Unknown)
        {
            throw new ArgumentException("A fixture cannot have a location of unknown.", nameof(location));
        }

        if (status == FixtureStatus.Unknown)
        {
            throw new ArgumentException("A fixture cannot have a status of unknown.", nameof(status));
        }

        if (status != FixtureStatus.Completed && score.HomeGoals != 0 && score.AwaysGoals != 0)
        {
            throw new ArgumentException("A fixture cannot have a score without a status of completed.", nameof(score));
        }
    }
}
