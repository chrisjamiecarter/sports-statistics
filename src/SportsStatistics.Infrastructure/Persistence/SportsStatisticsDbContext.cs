using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsStatistics.Infrastructure.Persistence.Models;

namespace SportsStatistics.Infrastructure.Persistence;

internal sealed class SportsStatisticsDbContext(DbContextOptions<SportsStatisticsDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
}
