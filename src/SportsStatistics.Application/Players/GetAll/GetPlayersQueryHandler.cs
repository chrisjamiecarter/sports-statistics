using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.GetAll;

internal sealed class GetPlayersQueryHandler(IPlayerRepository repository) : IQueryHandler<GetPlayersQuery, List<PlayerResponse>>
{
    private readonly IPlayerRepository _repository = repository;

    public async Task<Result<List<PlayerResponse>>> Handle(GetPlayersQuery query, CancellationToken cancellationToken)
    {
        var players = await _repository.GetAllAsync(cancellationToken);
        return players.ToResponse();
    }
}
