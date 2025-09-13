using SportsStatistics.Core.Application.Abstractions;
using SportsStatistics.Core.Results;

namespace SportsStatistics.Application.Players.Commands.UpdatePlayer;

public sealed record UpdatePlayerCommand(Guid Id,
                                         string Name,
                                         string Role,
                                         int SquadNumber,
                                         string Nationality,
                                         DateOnly DateOfBirth) : ICommand<Result>;
