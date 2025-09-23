using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Competitions.Create;

public sealed record CreateCompetitionCommand(string Name,
                                              string CompetitionType) : ICommand;
