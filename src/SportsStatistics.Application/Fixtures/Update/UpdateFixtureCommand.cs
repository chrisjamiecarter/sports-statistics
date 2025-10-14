using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.Update;

public sealed record UpdateFixtureCommand(Guid Id,
                                          string Opponent,
                                          DateTime KickoffTimeUtc,
                                          string FixtureLocationName) : ICommand;
