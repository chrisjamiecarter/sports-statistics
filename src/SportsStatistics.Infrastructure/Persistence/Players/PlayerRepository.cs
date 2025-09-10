using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Repositories;
using SportsStatistics.Domain.Entities;

namespace SportsStatistics.Infrastructure.Persistence.Players;

internal sealed class PlayerRepository(SportsStatisticsDbContext dbContext) : IPlayerRepository
{
    private readonly SportsStatisticsDbContext _dbContext = dbContext;

    public async Task<List<Player>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Players.AsNoTracking()
                                       .ToListAsync(cancellationToken);
    }
}
