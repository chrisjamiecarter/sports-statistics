using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.MatchEvents;

public class MatchEventType : Enumeration
{
    public static readonly MatchEventType Unknown = new(0, nameof(Unknown));
    public static readonly MatchEventType Kickoff = new(1, nameof(Kickoff));
    public static readonly MatchEventType HalfTime = new(2, nameof(HalfTime));
    public static readonly MatchEventType FullTime = new(3, nameof(FullTime));
    public static readonly MatchEventType HomeGoal = new(4, nameof(HomeGoal));
    public static readonly MatchEventType AwayGoal = new(5, nameof(AwayGoal));

    public MatchEventType(int id, string name) : base(id, name) { }

    public static IReadOnlyCollection<MatchEventType> All =>
    [
        Kickoff,
        HalfTime,
        FullTime,
        HomeGoal,
        AwayGoal
    ];

    public static MatchEventType FromName(string eventType)
    {
        return All.SingleOrDefault(t => string.Equals(t.Name, eventType, StringComparison.OrdinalIgnoreCase))
               ?? Unknown;
    }
}
