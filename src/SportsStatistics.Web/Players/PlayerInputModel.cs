namespace SportsStatistics.Web.Players;

public sealed class PlayerInputModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public int SquadNumber { get; set; }

    public string Nationality { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }
}
