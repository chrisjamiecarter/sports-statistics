using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.GetByFixtureId;

internal sealed class GetMatchEventsByFixtureIdQueryHandler(IApplicationDbContext dbContext) 
    : IQueryHandler<GetMatchEventsByFixtureIdQuery, List<MatchEventResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<MatchEventResponse>>> Handle(
        GetMatchEventsByFixtureIdQuery request, 
        CancellationToken cancellationToken)
    {
        var matchEvents = await _dbContext.MatchEvents
            .AsNoTracking()
            .Where(e => e.FixtureId == request.FixtureId)
            .Select(e => new MatchEventResponse(
                e.Id,
                e.FixtureId,
                e.Type.Name,
                e.Minute.Value,
                e.OccurredAtUtc,
                null,
                null))
            .ToListAsync(cancellationToken);

        var playerEvents = await _dbContext.PlayerEvents
            .AsNoTracking()
            .Where(e => e.FixtureId == request.FixtureId)
            .Join(
                _dbContext.Players.AsNoTracking(),
                pe => pe.PlayerId,
                p => p.Id,
                (pe, p) => new MatchEventResponse(
                    pe.Id,
                    pe.FixtureId,
                    pe.Type.Name,
                    pe.Minute.Value,
                    pe.OccurredAtUtc,
                    p.Name.Value,
                    pe.PlayerId))
            .ToListAsync(cancellationToken);

        var substitutionEvents = await _dbContext.SubstitutionEvents
            .AsNoTracking()
            .Where(e => e.FixtureId == request.FixtureId)
            .Select(e => new MatchEventResponse(
                e.Id,
                e.FixtureId,
                "Substitution",
                e.Minute.Value,
                e.OccurredAtUtc,
                null,
                null))
            .ToListAsync(cancellationToken);

        var allEvents = matchEvents
            .Concat(playerEvents)
            .Concat(substitutionEvents)
            .OrderBy(e => e.Minute)
            .ThenBy(e => e.OccurredAtUtc)
            .ToList();

        return allEvents;
    }
}
