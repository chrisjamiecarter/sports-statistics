using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.Update;

public sealed record UpdatePlayerCommand(Guid Id,
                                         string Name,
                                         int SquadNumber,
                                         string Nationality,
                                         DateOnly DateOfBirth,
                                         string PositionName) : ICommand;
