using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Seasons;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Persistence.Seasons;

internal sealed class SeasonRepository(SportsStatisticsDbContext dbContext) : ISeasonRepository
{
    private readonly SportsStatisticsDbContext _dbContext = dbContext;

    public async Task<bool> CreateAsync(Season season, CancellationToken cancellationToken)
    {
        _dbContext.Seasons.Add(season);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(Season season, CancellationToken cancellationToken)
    {
        _dbContext.Seasons.Remove(season);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<List<Season>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Seasons.AsNoTracking()
                                        .ToListAsync(cancellationToken);
    }

    public async Task<Season?> GetByIdAsync(EntityId id, CancellationToken cancellationToken)
    {
        //TODO: Confirm works. Alternative = return await _dbContext.Seasons.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
        return await _dbContext.Seasons.FindAsync([id], cancellationToken);
    }

    public async Task<bool> UpdateAsync(Season season, CancellationToken cancellationToken)
    {
        _dbContext.Seasons.Update(season);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
