using Microsoft.EntityFrameworkCore;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Competition> Competitions { get; }

    DbSet<Fixture> Fixtures { get; }

    DbSet<Player> Players { get; }

    DbSet<Season> Seasons { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
