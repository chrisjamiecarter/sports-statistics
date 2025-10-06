using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.Update;

public sealed record UpdateFixtureCommand(Guid Id,
                                          DateTime KickoffTimeUtc,
                                          string LocationName,
                                          int HomeGoals,
                                          int AwayGoals,
                                          string StatusName) : ICommand;
