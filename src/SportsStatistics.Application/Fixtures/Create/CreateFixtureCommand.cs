using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.Create;

public sealed record CreateFixtureCommand(DateTime KickoffTimeUtc,
                                          Guid CompetitionId,
                                          string FixtureLocation) : ICommand;
