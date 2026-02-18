using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.MatchTracking.PlayerEvents.Create;

public sealed record CreatePlayerEventCommand(Guid FixtureId,
                                              Guid PlayerId,
                                              int PlayerEventTypeId,
                                              int Minute,
                                              DateTime OccurredAtUtc) : ICommand;
