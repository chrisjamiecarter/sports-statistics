using Microsoft.EntityFrameworkCore;

namespace SportsStatistics.Infrastructure.Persistence;

internal sealed class SportsStatisticsDbContext(DbContextOptions<SportsStatisticsDbContext> options)
    : DbContext(options)
{
}
