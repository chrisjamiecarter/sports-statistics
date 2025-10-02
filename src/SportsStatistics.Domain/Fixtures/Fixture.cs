using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class Fixture : Entity
{
    private Fixture(EntityId id, DateTime kickoffTimeUtc, Competition competition, FixtureLocation location, FixtureScore score, FixtureStatus status) : base(id)
    {
        KickoffTimeUtc = kickoffTimeUtc;
        Competition = competition;
        Location = location;
        Score = score;
        Status = status;
    }

    public DateTime KickoffTimeUtc { get; private set; }

    public Competition Competition { get; private set; }
    
    public FixtureLocation Location { get; private set; } = FixtureLocation.Unknown;

    public FixtureScore Score { get; private set; }

    public FixtureStatus Status { get; private set; } = FixtureStatus.Unknown;

    public static Fixture Create(DateTime kickoffTimeUtc, Competition competition, FixtureLocation location, FixtureScore score, FixtureStatus status)
    {
        // TODO: Validation.
        return new Fixture(EntityId.Create(), kickoffTimeUtc, competition, location, score, status);
    }
}
