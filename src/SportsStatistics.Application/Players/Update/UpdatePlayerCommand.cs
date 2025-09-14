using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.Update;

public sealed record UpdatePlayerCommand(Guid Id,
                                         string Name,
                                         string Role,
                                         int SquadNumber,
                                         string Nationality,
                                         DateOnly DateOfBirth) : ICommand;
