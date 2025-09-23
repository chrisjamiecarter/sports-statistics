using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Competitions.GetAll;

public sealed record GetCompetitionsQuery() : IQuery<List<CompetitionResponse>>;
