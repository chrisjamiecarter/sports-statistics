using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.Create;

public sealed record CreateFixtureCommand(Guid CompetitionId,
                                          DateTime KickoffTimeUtc,                                          
                                          string FixtureLocation) : ICommand;
