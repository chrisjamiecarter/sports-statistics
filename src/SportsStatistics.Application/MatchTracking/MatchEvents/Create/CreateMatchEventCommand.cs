using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.MatchTracking.MatchEvents.Create;

public sealed record CreateMatchEventCommand(Guid FixtureId,
                                             int MatchEventTypeId,
                                             int Minute,
                                             DateTime OccurredAtUtc) : ICommand;
