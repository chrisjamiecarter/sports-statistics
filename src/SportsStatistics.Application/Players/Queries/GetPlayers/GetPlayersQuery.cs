using SportsStatistics.Core.Application.Abstractions;

namespace SportsStatistics.Application.Players.Queries.GetPlayers;

public sealed record GetPlayersQuery() : IQuery<List<GetPlayersQueryResponse>>;
