using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.GetRecentForm;

public sealed record GetRecentFormQuery(
    Guid SeasonId,
    int Count = 5)
    : IQuery<List<FormReponse>>;
