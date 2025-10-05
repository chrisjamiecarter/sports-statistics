using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.Delete;

public sealed record DeleteFixtureCommand(Guid Id) : ICommand;
