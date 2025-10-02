namespace SportsStatistics.Domain.Fixtures;

public sealed class FixtureScore
{
    private FixtureScore(int homeGoals, int awayGoals)
    {
        HomeGoals = homeGoals;
        AwaysGoals = awayGoals;
    }

    public int HomeGoals { get; private set; }

    public int AwaysGoals { get; private set; }

    public static FixtureScore Create(int homeGoals, int awayGoals)
    {
        // TODO: Validation.
        return new(homeGoals, awayGoals);
    }
}