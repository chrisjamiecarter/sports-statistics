using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players;

public interface IPlayerRepository
{
    Task<bool> CreateAsync(Player player, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Player player, CancellationToken cancellationToken);
    Task<List<Player>> GetAllAsync(CancellationToken cancellationToken);
    Task<Player?> GetByIdAsync(EntityId id, CancellationToken cancellationToken);
    Task<bool> IsSquadNumberAvailableAsync(int squadNumber, EntityId? excludingPlayerId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Player player, CancellationToken cancellationToken);
}
