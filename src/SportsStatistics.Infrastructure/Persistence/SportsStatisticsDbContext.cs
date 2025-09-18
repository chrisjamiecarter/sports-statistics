using Microsoft.EntityFrameworkCore;
using SportsStatistics.Domain.Players;

namespace SportsStatistics.Infrastructure.Persistence;

internal sealed class SportsStatisticsDbContext(DbContextOptions<SportsStatisticsDbContext> options) : DbContext(options)
{
    internal DbSet<Player> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
