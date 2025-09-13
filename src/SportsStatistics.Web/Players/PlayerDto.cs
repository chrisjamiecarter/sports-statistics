namespace SportsStatistics.Web.Players;

public sealed record PlayerDto(Guid Id,
                               string Name,
                               string Role,
                               int SquadNumber,
                               string Nationality,
                               DateOnly DateOfBirth,
                               int Age);
