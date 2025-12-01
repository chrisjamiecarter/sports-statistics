using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed record Score
{
    private Score(int homeGoals, int awayGoals)
    {
        HomeGoals = homeGoals;
        AwayGoals = awayGoals;
    }

    public int HomeGoals { get; private set; }

    public int AwayGoals { get; private set; }

    public static Result<Score> Create(int homeGoals, int awayGoals)
    {
        // TODO: Validation.
        return new Score(homeGoals, awayGoals);
    }
}