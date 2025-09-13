using SportsStatistics.Domain.Entities;

namespace SportsStatistics.Application.Players;

public interface IPlayerRepository
{
    Task<bool> CreateAsync(Player player, CancellationToken cancellationToken);
    Task<List<Player>> GetAllAsync(CancellationToken cancellationToken);
}
