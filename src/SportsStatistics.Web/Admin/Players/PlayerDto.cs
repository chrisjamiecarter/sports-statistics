namespace SportsStatistics.Web.Admin.Players;

public sealed record PlayerDto(Guid Id,
                               string Name,
                               int SquadNumber,
                               string Nationality,
                               DateOnly DateOfBirth,
                               string Position,
                               int Age);
