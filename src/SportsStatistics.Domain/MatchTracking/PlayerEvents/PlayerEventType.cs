using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.PlayerEvents;

public sealed class PlayerEventType : Enumeration
{
    public static readonly PlayerEventType Unknown = new(0, nameof(Unknown));
    public static readonly PlayerEventType PassSuccess = new(1, nameof(PassSuccess));
    public static readonly PlayerEventType PassFailure = new(2, nameof(PassFailure));
    public static readonly PlayerEventType ShotOnTarget = new(3, nameof(ShotOnTarget));
    public static readonly PlayerEventType ShotOffTarget = new(4, nameof(ShotOffTarget));
    public static readonly PlayerEventType Goal = new(5, nameof(Goal));
    public static readonly PlayerEventType GoalAssist = new(6, nameof(GoalAssist));
    public static readonly PlayerEventType OwnGoal = new(7, nameof(OwnGoal));
    public static readonly PlayerEventType Save = new(8, nameof(Save));
    public static readonly PlayerEventType Tackle = new(9, nameof(Tackle));
    public static readonly PlayerEventType FoulWon = new(10, nameof(FoulWon));
    public static readonly PlayerEventType FoulConceeded = new(11, nameof(FoulConceeded));
    public static readonly PlayerEventType YellowCard = new(12, nameof(YellowCard));
    public static readonly PlayerEventType RedCard = new(13, nameof(RedCard));

    private PlayerEventType(int id, string name) : base(id, name) { }

    public static IReadOnlyCollection<PlayerEventType> All =>
    [
        PassSuccess,
        PassFailure,
        ShotOnTarget,
        ShotOffTarget,
        Goal,
        GoalAssist,
        OwnGoal,
        Save,
        Tackle,
        FoulWon,
        FoulConceeded,
        YellowCard,
        RedCard
    ];

    public static PlayerEventType FromName(string eventType)
    {
        return All.SingleOrDefault(t => string.Equals(t.Name, eventType, StringComparison.OrdinalIgnoreCase))
               ?? Unknown;
    }
}
