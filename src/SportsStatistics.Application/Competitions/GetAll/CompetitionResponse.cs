namespace SportsStatistics.Application.Competitions.GetAll;

public sealed record CompetitionResponse(Guid Id,
                                         string Name,
                                         string CompetitionType);
