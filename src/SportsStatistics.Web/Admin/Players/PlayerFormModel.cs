namespace SportsStatistics.Web.Admin.Players;

public sealed class PlayerFormModel
{
    public string Name { get; set; } = "Joe Bloggs";//string.Empty;

    public int SquadNumber { get; set; } = 1;

    public string Nationality { get; set; } = "British";//string.Empty;

    public DateTime? DateOfBirth { get; set; } = DateTime.UtcNow.AddYears(-21);

    public PositionOptionDto? Position { get; set; }
}
