using SportsStatistics.Application.Tests.Competitions;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Tests.Fixtures;

public class FixtureBuilder : IBuildable<Fixture>
{
    private Competition _competition = new CompetitionBuilder().Build();
    private string _opponent = "Test Opponent";
    private DateTime _kickoffTimeUtc = DateTime.UtcNow;
    private Location _location = Location.Home;

    public FixtureBuilder WithCompetition(Competition competition)
    {
        _competition = competition;
        return this;
    }

    public FixtureBuilder WithOpponent(string opponent)
    {
        _opponent = opponent;
        return this;
    }

    public FixtureBuilder WithKickoffTimeUtc(DateTime kickoffTimeUtc)
    {
        _kickoffTimeUtc = kickoffTimeUtc;
        return this;
    }

    public FixtureBuilder WithLocation(Location location)
    {
        _location = location;
        return this;
    }

    private static Fixture Create(Competition competition, string opponent, DateTime kickoffTimeUtc, Location location) =>
        Fixture.Create(
            competition,
            Opponent.Create(opponent).Value,
            KickoffTimeUtc.Create(kickoffTimeUtc).Value,
            location);

    public Fixture Build() =>
        Create(_competition, _opponent, _kickoffTimeUtc, _location);

    public static List<Fixture> GetDefaults()
    {
        var builder = new FixtureBuilder();

        return
        [
            builder.WithOpponent("League Opponent One").WithKickoffTimeUtc(DateTime.UtcNow.AddDays(7)).WithLocation(Location.Home).Build(),
            builder.WithOpponent("League Opponent Two").WithKickoffTimeUtc(DateTime.UtcNow.AddDays(14)).WithLocation(Location.Away).Build(),
        ];
    }
}
