using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Seasons;

public interface ISeasonRepository
{
    Task<bool> CreateAsync(Season season, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Season season, CancellationToken cancellationToken);
    Task<List<Season>> GetAllAsync(CancellationToken cancellationToken);
    Task<Season?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Season season, CancellationToken cancellationToken);
}
