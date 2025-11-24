namespace SportsStatistics.Application.Players.GetAll;

public sealed record PlayerResponse(Guid Id,
                                    string Name,
                                    int SquadNumber,
                                    string Nationality,
                                    DateOnly DateOfBirth,
                                    int PositionId,
                                    string Position,
                                    int Age);
