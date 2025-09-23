using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions;

public interface ICompetitionRepository
{
    Task<bool> CreateAsync(Competition competition, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Competition competition, CancellationToken cancellationToken);
    Task<List<Competition>> GetAllAsync(CancellationToken cancellationToken);
    Task<Competition?> GetByIdAsync(EntityId id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Competition competition, CancellationToken cancellationToken);
}
