using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.GetAll;

internal sealed class GetAllPlayersQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetAllPlayersQuery, List<PlayerResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<PlayerResponse>>> Handle(GetAllPlayersQuery query, CancellationToken cancellationToken)
    {
        var players = await _dbContext.Players.AsNoTracking()
                                              .ToListAsync(cancellationToken);

        return players.ToResponse();
    }
}
