using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.Delete;

public sealed record DeletePlayerCommand(Guid Id) : ICommand;
