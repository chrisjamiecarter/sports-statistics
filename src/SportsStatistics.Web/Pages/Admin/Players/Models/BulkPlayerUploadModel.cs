namespace SportsStatistics.Web.Pages.Admin.Players.Models;

public sealed record BulkPlayerUploadModel(string? Name, int? SquadNumber, string? Nationality, DateTime? DateOfBirth, string? Position);
