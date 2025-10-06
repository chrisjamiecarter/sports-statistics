using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public interface IPlayerService
{
    Task<bool> IsSquadNumberAvailableAsync(EntityId currentPlayerId, int squadNumber, CancellationToken cancellationToken);
}
