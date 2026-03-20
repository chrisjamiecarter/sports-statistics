using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Reports.GetPlayerSeasonStatistics;

internal sealed class GetPlayerSeasonStatisticsQueryHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetPlayerSeasonStatisticsQuery, List<PlayerSeasonStatisticsResponse>>
{
    private sealed class PlayerAppearances(int starts, int substitutions)
    {
        public int Starts { get; set; } = starts;
        public int Substitutions { get; set; } = substitutions;

        public int Total => Starts + Substitutions;
    }

    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<PlayerSeasonStatisticsResponse>>> Handle(GetPlayerSeasonStatisticsQuery request, CancellationToken cancellationToken)
    {
        var completedFixtureIds = await _dbContext.Fixtures
            .AsNoTracking()
            .Where(fixture => fixture.Status == Status.Completed)
            .Join(_dbContext.Competitions
                .AsNoTracking()
                .Where(competition => competition.SeasonId == request.SeasonId),
                    fixture => fixture.CompetitionId,
                    competition => competition.Id,
                    (fixture, _) => fixture)
            .Select(fixture => fixture.Id)
            .ToListAsync(cancellationToken);

        if (completedFixtureIds.Count == 0)
        {
            return new List<PlayerSeasonStatisticsResponse>();
        }

        var teamsheets = await _dbContext.Teamsheets
            .AsNoTracking()
            .Include(teamsheet => teamsheet.Players)
            .Where(teamsheet => completedFixtureIds.Contains(teamsheet.FixtureId))
            .ToDictionaryAsync(teamsheet => teamsheet.FixtureId, cancellationToken);

        var playerEvents = await _dbContext.PlayerEvents
            .AsNoTracking()
            .Where(playerEvent => completedFixtureIds.Contains(playerEvent.FixtureId))
            .ToListAsync(cancellationToken);

        var substitutionEvents = await _dbContext.SubstitutionEvents
            .AsNoTracking()
            .Where(substitutionEvent => completedFixtureIds.Contains(substitutionEvent.FixtureId))
            .ToListAsync(cancellationToken);

        var players = await _dbContext.Players
            .AsNoTracking()
            .ToDictionaryAsync(player => player.Id, cancellationToken);

        var playerAppearances = new Dictionary<Guid, PlayerAppearances>();
        foreach (var fixtureId in completedFixtureIds)
        {
            var teamsheet = teamsheets[fixtureId];
            foreach (var playerId in teamsheet?.GetStarterIds() ?? [])
            {
                if (playerAppearances.TryGetValue(playerId, out var playerAppearance))
                {
                    playerAppearance.Starts++;
                }
                else
                {
                    playerAppearances.TryAdd(playerId, new PlayerAppearances(1, 0));
                }
            }
        }

        foreach (var substitutionEvent in substitutionEvents)
        {
            var playerId = substitutionEvent.Substitution.PlayerOnId;
            if (playerAppearances.TryGetValue(playerId, out var playerAppearance))
            {
                playerAppearance.Substitutions++;
            }
            else
            {
                playerAppearances.TryAdd(playerId, new PlayerAppearances(0, 1));
            }
        }

        var statistics = playerEvents
            .GroupBy(playerEvent => playerEvent.PlayerId)
            .Select(group =>
            {
                var player = players[group.Key];
                var appearances = playerAppearances[group.Key];
                return new PlayerSeasonStatisticsResponse(
                    player.Id,
                    player.Name,
                    player.SquadNumber,
                    player.Position,
                    appearances?.Starts ?? 0,
                    appearances?.Total ?? 0,
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.PassSuccess),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.PassFailure),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.ShotOnTarget),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.ShotOffTarget),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.Goal),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.GoalAssist),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.OwnGoal),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.Save),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.Tackle),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.FoulWon),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.FoulConceded),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.YellowCard),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.RedCard));
            });

        return statistics.ToList();
    }    
}
