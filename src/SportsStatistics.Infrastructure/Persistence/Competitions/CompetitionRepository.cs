using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Competitions;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Persistence.Competitions;
internal sealed class CompetitionRepository(SportsStatisticsDbContext dbContext) : ICompetitionRepository
{
    private readonly SportsStatisticsDbContext _dbContext = dbContext;

    public async Task<bool> CreateAsync(Competition competition, CancellationToken cancellationToken)
    {
        _dbContext.Competitions.Add(competition);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(Competition competition, CancellationToken cancellationToken)
    {
        _dbContext.Competitions.Remove(competition);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<List<Competition>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Competitions.AsNoTracking()
                                            .ToListAsync(cancellationToken);
    }

    public async Task<Competition?> GetByIdAsync(EntityId id, CancellationToken cancellationToken)
    {
        return await _dbContext.Competitions.FindAsync([id], cancellationToken);
    }

    public async Task<bool> UpdateAsync(Competition competition, CancellationToken cancellationToken)
    {
        _dbContext.Competitions.Update(competition);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
