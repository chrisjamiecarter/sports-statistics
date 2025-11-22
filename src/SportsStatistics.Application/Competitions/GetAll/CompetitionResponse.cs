namespace SportsStatistics.Application.Competitions.GetAll;

public sealed record CompetitionResponse(Guid Id,
                                         Guid SeasonId,
                                         string Name,
                                         int FormatId,
                                         string Format);
