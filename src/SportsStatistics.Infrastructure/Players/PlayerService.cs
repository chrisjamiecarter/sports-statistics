using Microsoft.EntityFrameworkCore;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Players;

internal class PlayerService(ApplicationDbContext dbContext) : IPlayerService
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<bool> IsSquadNumberAvailableAsync(EntityId currentPlayerId, int squadNumber, CancellationToken cancellationToken)
    {
        var taken = await _dbContext.Players.AsNoTracking()
                                            .AnyAsync(p => p.Id != currentPlayerId && p.SquadNumber == squadNumber, cancellationToken);
        return !taken;
    }
}
