using SportsStatistics.Application.Abstractions.Repositories;
using SportsStatistics.Core.Application.Abstractions;

namespace SportsStatistics.Application.Players.Queries.GetPlayers;

public sealed class GetPlayersQueryHandler(IPlayerRepository repository) : IQueryHandler<GetPlayersQuery, List<GetPlayersQueryResponse>>
{
    private readonly IPlayerRepository _repository = repository;

    public async Task<List<GetPlayersQueryResponse>> HandleAsync(GetPlayersQuery query, CancellationToken cancellationToken)
    {
        var players = await _repository.GetAllAsync(cancellationToken);
        return players.ToResponse();
    }
}
