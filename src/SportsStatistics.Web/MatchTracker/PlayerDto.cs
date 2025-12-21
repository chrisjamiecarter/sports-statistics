namespace SportsStatistics.Web.MatchTracker;

public sealed record PlayerDto(Guid Id,
                               string Name,
                               int SquadNumber,
                               string Nationality,
                               DateOnly DateOfBirth,
                               int PostionId,
                               string Position,
                               int Age);
