using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Competitions.Update;

public sealed record UpdateCompetitionCommand(Guid CompetitionId,
                                              string Name,
                                              string CompetitionTypeName) : ICommand;
