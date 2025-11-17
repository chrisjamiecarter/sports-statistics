using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed class Position : Enumeration
{
    public static readonly Position Unknown = new(0, nameof(Unknown));
    public static readonly Position Goalkeeper = new(1, nameof(Goalkeeper));
    public static readonly Position Defender = new(2, nameof(Defender));
    public static readonly Position Midfielder = new(3, nameof(Midfielder));
    public static readonly Position Attacker = new(4, nameof(Attacker));

    private Position(int id, string name) : base(id, name) { }

    public static IReadOnlyCollection<Position> All =>
    [
        Goalkeeper,
        Defender,
        Midfielder,
        Attacker
    ];

    public static int MaxLength => All.Max(p => p.Name.Length);

    public static Position FromName(string position)
    {
        return All.SingleOrDefault(p => string.Equals(p.Name, position, StringComparison.OrdinalIgnoreCase))
               ?? Unknown;
    }
}
