using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.GetByFixtureId;

internal sealed class GetMatchEventsByFixtureIdQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetMatchEventsByFixtureIdQuery, List<MatchEventResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<MatchEventResponse>>> Handle(GetMatchEventsByFixtureIdQuery request, CancellationToken cancellationToken)
    {
        var matchEvents = await _dbContext.MatchEvents
            .AsNoTracking()
            .Where(e => e.FixtureId == request.FixtureId)
            .Select(e => new
            {
                e.Id,
                e.FixtureId,
                TypeName = e.Type.Name,
                e.Minute.BaseMinute,
                e.Minute.StoppageMinute,
                DisplayMinute = e.Minute.DisplayNotation,
                e.OccurredAtUtc
            })
            .ToListAsync(cancellationToken);

        var matchEventResponses = matchEvents
            .Select(e => new MatchEventResponse(
                e.Id,
                e.FixtureId,
                e.TypeName,
                e.BaseMinute,
                e.StoppageMinute,
                e.DisplayMinute,
                DerivePeriodName(e.BaseMinute, e.StoppageMinute),
                e.OccurredAtUtc,
                null,
                null))
            .ToList();

        var playerEventData = await _dbContext.PlayerEvents
            .AsNoTracking()
            .Where(e => e.FixtureId == request.FixtureId)
            .Join(
                _dbContext.Players.AsNoTracking(),
                pe => pe.PlayerId,
                p => p.Id,
                (pe, p) => new
                {
                    pe.Id,
                    pe.FixtureId,
                    TypeName = pe.Type.Name,
                    pe.Minute.BaseMinute,
                    pe.Minute.StoppageMinute,
                    DisplayMinute = pe.Minute.DisplayNotation,
                    pe.OccurredAtUtc,
                    PlayerName = p.Name.Value,
                    pe.PlayerId
                })
            .ToListAsync(cancellationToken);

        var playerEventResponses = playerEventData
            .Select(e => new MatchEventResponse(
                e.Id,
                e.FixtureId,
                e.TypeName,
                e.BaseMinute,
                e.StoppageMinute,
                e.DisplayMinute,
                DerivePeriodName(e.BaseMinute, e.StoppageMinute),
                e.OccurredAtUtc,
                e.PlayerName,
                e.PlayerId))
            .ToList();

        var substitutionEventData = await _dbContext.SubstitutionEvents
            .AsNoTracking()
            .Where(e => e.FixtureId == request.FixtureId)
            .Join(_dbContext.Players.AsNoTracking(),
                e => e.Substitution.PlayerOffId,
                p => p.Id,
                (e, playerOff) => new
                {
                    e.Id,
                    e.FixtureId,
                    e.Minute.BaseMinute,
                    e.Minute.StoppageMinute,
                    DisplayMinute = e.Minute.DisplayNotation,
                    e.OccurredAtUtc,
                    PlayerOffName = playerOff.Name.Value,
                    e.Substitution.PlayerOffId,
                    e.Substitution.PlayerOnId
                })
            .Join(_dbContext.Players.AsNoTracking(),
                x => x.PlayerOnId,
                p => p.Id,
                (x, playerOn) => new
                {
                    x.Id,
                    x.FixtureId,
                    x.BaseMinute,
                    x.StoppageMinute,
                    x.DisplayMinute,
                    x.OccurredAtUtc,
                    PlayerName = $"{x.PlayerOffName} → {playerOn.Name.Value}",
                    x.PlayerOffId
                })
            .ToListAsync(cancellationToken);

        var substitutionEventResponses = substitutionEventData
            .Select(e => new MatchEventResponse(
                e.Id,
                e.FixtureId,
                "Substitution",
                e.BaseMinute,
                e.StoppageMinute,
                e.DisplayMinute,
                DerivePeriodName(e.BaseMinute, e.StoppageMinute),
                e.OccurredAtUtc,
                e.PlayerName,
                e.PlayerOffId))
            .ToList();

        var allEvents = matchEventResponses
            .Concat(playerEventResponses)
            .Concat(substitutionEventResponses)
            .OrderBy(e => e.BaseMinute)
            .ThenBy(e => e.StoppageMinute ?? 0)
            .ThenBy(e => e.OccurredAtUtc)
            .ToList();

        return allEvents;
    }

    private static string DerivePeriodName(int baseMinute, int? stoppageMinute)
    {
        if (stoppageMinute.HasValue)
        {
            return baseMinute switch
            {
                45 => "FirstHalfStoppage",
                90 => "SecondHalfStoppage",
                _ => "FirstHalf"
            };
        }

        return baseMinute switch
        {
            >= 1 and <= 45 => "FirstHalf",
            >= 46 and <= 90 => "SecondHalf",
            _ => "FirstHalf"
        };
    }
}
