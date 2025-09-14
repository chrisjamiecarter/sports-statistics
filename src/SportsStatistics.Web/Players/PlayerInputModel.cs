namespace SportsStatistics.Web.Players;

public sealed class PlayerInputModel
{
    public string Name { get; set; } = string.Empty;

    public int SquadNumber { get; set; }

    public string Nationality { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public string Position { get; set; } = string.Empty;
}
