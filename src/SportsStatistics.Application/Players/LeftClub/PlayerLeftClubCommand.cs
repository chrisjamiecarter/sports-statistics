using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.LeftClub;

public sealed record PlayerLeftClubCommand(Guid PlayerId) : ICommand;
