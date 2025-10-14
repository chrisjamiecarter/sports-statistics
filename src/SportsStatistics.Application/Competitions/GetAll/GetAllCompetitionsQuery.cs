using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Competitions.GetAll;

public sealed record GetAllCompetitionsQuery() : IQuery<List<CompetitionResponse>>;
