using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Fixtures;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Fixtures;

internal sealed class FixtureRepository(ApplicationDbContext dbContext) : IFixtureRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<bool> CreateAsync(Fixture fixture, CancellationToken cancellationToken)
    {
        _dbContext.Fixtures.Add(fixture);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(Fixture fixture, CancellationToken cancellationToken)
    {
        _dbContext.Fixtures.Remove(fixture);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<List<Fixture>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Fixtures.AsNoTracking()
                                        .ToListAsync(cancellationToken);
    }

    public async Task<Fixture?> GetByIdAsync(EntityId id, CancellationToken cancellationToken)
    {
        return await _dbContext.Fixtures.FindAsync([id], cancellationToken);
    }

    public async Task<bool> UpdateAsync(Fixture fixture, CancellationToken cancellationToken)
    {
        _dbContext.Fixtures.Update(fixture);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
