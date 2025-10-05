using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.GetAll;

public sealed record GetFixturesQuery() : IQuery<List<FixtureResponse>>;
