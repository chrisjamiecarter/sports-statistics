using SportsStatistics.Common.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.Queries.GetPlayers;

public sealed record GetPlayersQuery() : IQuery<List<GetPlayersQueryResponse>>;
