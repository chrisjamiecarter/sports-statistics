using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Competitions.Delete;

public sealed record DeleteCompetitionCommand(Guid Id) : ICommand;
