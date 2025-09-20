using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Players;
using SportsStatistics.Domain.Players;

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

    public async Task<bool> DeleteAsync(Player player, CancellationToken cancellationToken)
    {
        _dbContext.Players.Remove(player);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<List<Player>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Players.AsNoTracking()
                                        .ToListAsync(cancellationToken);
    }

    public async Task<Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Players.FindAsync([id], cancellationToken);
    }

    public async Task<bool> IsSquadNumberAvailableAsync(int squadNumber, Guid? excludingPlayerId, CancellationToken cancellationToken)
    {
        var taken = await _dbContext.Players.AsNoTracking()
                                            .AnyAsync(p => p.SquadNumber == squadNumber && p.Id != excludingPlayerId, cancellationToken);
        return !taken;
    }

    public async Task<bool> UpdateAsync(Player player, CancellationToken cancellationToken)
    {
        _dbContext.Players.Update(player);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
