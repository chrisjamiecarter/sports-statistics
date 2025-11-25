using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.PlayerEvents;

public sealed class PlayerEventType : Enumeration<PlayerEventType>
{
    public static readonly PlayerEventType PassSuccess = new(0, nameof(PassSuccess));
    public static readonly PlayerEventType PassFailure = new(1, nameof(PassFailure));
    public static readonly PlayerEventType ShotOnTarget = new(2, nameof(ShotOnTarget));
    public static readonly PlayerEventType ShotOffTarget = new(3, nameof(ShotOffTarget));
    public static readonly PlayerEventType Goal = new(4, nameof(Goal));
    public static readonly PlayerEventType GoalAssist = new(5, nameof(GoalAssist));
    public static readonly PlayerEventType OwnGoal = new(6, nameof(OwnGoal));
    public static readonly PlayerEventType Save = new(7, nameof(Save));
    public static readonly PlayerEventType Tackle = new(8, nameof(Tackle));
    public static readonly PlayerEventType FoulWon = new(9, nameof(FoulWon));
    public static readonly PlayerEventType FoulConceeded = new(10, nameof(FoulConceeded));
    public static readonly PlayerEventType YellowCard = new(11, nameof(YellowCard));
    public static readonly PlayerEventType RedCard = new(12, nameof(RedCard));

    private PlayerEventType(int id, string name) : base(id, name) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEventType"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private PlayerEventType() { }
}
