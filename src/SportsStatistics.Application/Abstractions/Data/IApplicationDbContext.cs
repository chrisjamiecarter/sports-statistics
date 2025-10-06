using Microsoft.EntityFrameworkCore;
using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Player> Players { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
