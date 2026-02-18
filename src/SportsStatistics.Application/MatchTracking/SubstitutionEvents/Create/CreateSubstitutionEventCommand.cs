using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.MatchTracking.SubstitutionEvents.Create;

public sealed record CreateSubstitutionEventCommand(Guid FixtureId,
                                                    Guid PlayerOffId,
                                                    Guid PlayerOnId,
                                                    int Minute,
                                                    DateTime OccurredAtUtc) : ICommand;
