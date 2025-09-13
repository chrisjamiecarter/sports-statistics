using SportsStatistics.Common.Abstractions.Messaging;
using SportsStatistics.Common.Primitives.Results;

namespace SportsStatistics.Application.Players.Commands.UpdatePlayer;

public sealed record UpdatePlayerCommand(Guid Id,
                                         string Name,
                                         string Role,
                                         int SquadNumber,
                                         string Nationality,
                                         DateOnly DateOfBirth) : ICommand;
