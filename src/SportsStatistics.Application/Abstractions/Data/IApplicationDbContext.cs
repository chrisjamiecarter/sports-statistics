using Microsoft.EntityFrameworkCore;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Competition> Competitions { get; }

    DbSet<Fixture> Fixtures { get; }

    DbSet<MatchEvent> MatchEvents { get; }

    DbSet<Player> Players { get; }

    DbSet<PlayerEvent> PlayerEvents { get; }

    DbSet<Season> Seasons { get; }

    DbSet<SubstitutionEvent> SubstitutionEvents { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
