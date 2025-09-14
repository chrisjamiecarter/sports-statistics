namespace SportsStatistics.Application.Players.GetAll;

public sealed record PlayerResponse(Guid Id,
                                             string Name,
                                             string Role,
                                             int SquadNumber,
                                             string Nationalty,
                                             DateOnly DateOfBirth,
                                             int Age);
