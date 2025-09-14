using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.Create;

public sealed record CreatePlayerCommand(string Name,
                                         string Role,
                                         int SquadNumber,
                                         string Nationality,
                                         DateOnly DateOfBirth) : ICommand;
