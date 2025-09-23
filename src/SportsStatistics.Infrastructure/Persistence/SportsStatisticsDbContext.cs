using Microsoft.EntityFrameworkCore;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Infrastructure.Persistence;

internal sealed class SportsStatisticsDbContext(DbContextOptions<SportsStatisticsDbContext> options) : DbContext(options)
{
    public DbSet<Competition> Competitions { get; set; }

    public DbSet<Player> Players { get; set; }

    public DbSet<Season> Seasons { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
