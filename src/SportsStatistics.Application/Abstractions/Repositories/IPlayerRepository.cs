using SportsStatistics.Domain.Entities;

namespace SportsStatistics.Application.Abstractions.Repositories;

public interface IPlayerRepository
{
    Task<List<Player>> GetAllAsync(CancellationToken cancellationToken);
}
