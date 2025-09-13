using SportsStatistics.Core.Application.Abstractions;
using SportsStatistics.Core.Results;

namespace SportsStatistics.Application.Players.Commands.CreatePlayer;

public sealed record CreatePlayerCommand(string Name,
                                         string Role,
                                         int SquadNumber,
                                         string Nationality,
                                         DateOnly DateOfBirth) : ICommand<Result>;
