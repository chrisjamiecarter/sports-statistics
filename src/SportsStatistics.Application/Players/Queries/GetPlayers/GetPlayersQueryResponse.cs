namespace SportsStatistics.Application.Players.Queries.GetPlayers;

public sealed record GetPlayersQueryResponse(Guid Id,
                                             string Name,
                                             string Role,
                                             int SquadNumber,
                                             string Nationalty,
                                             DateOnly DateOfBirth,
                                             int Age);
