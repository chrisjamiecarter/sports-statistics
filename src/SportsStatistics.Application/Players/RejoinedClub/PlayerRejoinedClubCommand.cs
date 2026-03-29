using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.RejoinedClub;

public sealed record PlayerRejoinedClubCommand(Guid PlayerId) : ICommand;
