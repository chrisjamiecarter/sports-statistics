namespace SportsStatistics.Web.Pages.Admin.Players;

public sealed record PlayerDto(Guid Id,
                               string Name,
                               int SquadNumber,
                               string Nationality,
                               DateOnly DateOfBirth,
                               int PositionId,
                               string Position,
                               int Age);
