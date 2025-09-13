using SportsStatistics.Common.Abstractions.Messaging;
using SportsStatistics.Common.Primitives.Results;

namespace SportsStatistics.Application.Players.Queries.GetPlayers;

internal sealed class GetPlayersQueryHandler(IPlayerRepository repository) : IQueryHandler<GetPlayersQuery, List<GetPlayersQueryResponse>>
{
    private readonly IPlayerRepository _repository = repository;

    public async Task<Result<List<GetPlayersQueryResponse>>> Handle(GetPlayersQuery query, CancellationToken cancellationToken)
    {
        var players = await _repository.GetAllAsync(cancellationToken);
        return players.ToResponse();
    }
}
