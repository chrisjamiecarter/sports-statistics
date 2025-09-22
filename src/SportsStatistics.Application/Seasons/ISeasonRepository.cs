using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Seasons;

public interface ISeasonRepository
{
    Task<bool> CreateAsync(Season season, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Season season, CancellationToken cancellationToken);
    Task<bool> DoesDateOverlapExistingAsync(DateOnly startOrEndDate, EntityId? excludingSeasonId, CancellationToken cancellationToken);
    Task<List<Season>> GetAllAsync(CancellationToken cancellationToken);
    Task<Season?> GetByIdAsync(EntityId id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Season season, CancellationToken cancellationToken);
}
