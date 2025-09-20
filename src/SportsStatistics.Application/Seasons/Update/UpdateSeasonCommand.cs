using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Seasons.Update;

public sealed record UpdateSeasonCommand(Guid Id,
                                         DateOnly StartDate,
                                         DateOnly EndDate) : ICommand;
