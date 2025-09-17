using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.Delete;

public record DeletePlayerCommand(Guid Id) : ICommand;
