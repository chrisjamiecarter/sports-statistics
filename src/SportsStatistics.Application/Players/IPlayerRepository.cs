using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Players;

public interface IPlayerRepository
{
    Task<bool> CreateAsync(Player player, CancellationToken cancellationToken);
    Task<List<Player>> GetAllAsync(CancellationToken cancellationToken);
    Task<Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Player player, CancellationToken cancellationToken);
}
