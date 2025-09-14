using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.GetAll;

public sealed record GetPlayersQuery() : IQuery<List<PlayerResponse>>;
