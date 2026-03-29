namespace SportsStatistics.Web.Pages.Admin.Players.Models;

public sealed class BulkPlayerUploadInputModel
{
    public string? Name { get; set; }
    public int? SquadNumber { get; set; }
    public string? Nationality { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public PositionOptionDto? Position { get; set; }
}
