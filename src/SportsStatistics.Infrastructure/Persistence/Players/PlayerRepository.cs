using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Players;
using SportsStatistics.Domain.Entities;

namespace SportsStatistics.Infrastructure.Persistence.Players;

internal sealed class PlayerRepository(SportsStatisticsDbContext dbContext) : IPlayerRepository
{
    private readonly SportsStatisticsDbContext _dbContext = dbContext;

    public async Task<bool> CreateAsync(Player player, CancellationToken cancellationToken)
    {
        _dbContext.Players.Add(player);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<List<Player>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Players.AsNoTracking()
                                       .ToListAsync(cancellationToken);
    }
}
