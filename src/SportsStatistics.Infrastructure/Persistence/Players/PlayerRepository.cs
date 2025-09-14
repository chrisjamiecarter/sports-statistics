using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Players;
using SportsStatistics.Domain.Players;

namespace SportsStatistics.Infrastructure.Persistence.Players;

internal sealed class PlayerRepository(SportsStatisticsDbContext dbContext) : IPlayerRepository
{
    private readonly SportsStatisticsDbContext _dbContext = dbContext;

    public async Task<bool> CreateAsync(Player player, CancellationToken cancellationToken)
    {
        try
        {
            _dbContext.Players.Add(player);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
        catch (Exception)
        {
            // TODO: log exception?
            return false;
        }
    }

    public async Task<List<Player>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _dbContext.Players.AsNoTracking()
                                           .ToListAsync(cancellationToken);
        }
        catch (Exception)
        {
            // TODO: log exception?
            return [];
        }
    }

    public async Task<Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await _dbContext.Players.FindAsync([id], cancellationToken);
        }
        catch (Exception)
        {
            // TODO: log exception?
            return null;
        }
    }

    public async Task<bool> UpdateAsync(Player player, CancellationToken cancellationToken)
    {
        try
        {
            _dbContext.Players.Update(player);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
        catch (Exception)
        {
            // TODO: log exception?
            return false;
        }
    }
}
