namespace SportsStatistics.Application.Players.GetAll;

public sealed record PlayerResponse(Guid Id,
                                    string Name,
                                    int SquadNumber,
                                    string Nationality,
                                    DateOnly DateOfBirth,
                                    string Position,
                                    int Age);
