using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Reports.GetPlayerFixtureStatistics;

internal sealed class GetPlayerFixtureStatisticsQueryHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetPlayerFixtureStatisticsQuery, List<PlayerFixtureStatisticsResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<PlayerFixtureStatisticsResponse>>> Handle(GetPlayerFixtureStatisticsQuery request, CancellationToken cancellationToken)
    {
        var response = new List<PlayerFixtureStatisticsResponse>();

        var teamsheet = await _dbContext.Teamsheets
            .AsNoTracking()
            .Include(teamsheet => teamsheet.Players)
            .Where(teamsheet => teamsheet.FixtureId == request.FixtureId)
            .SingleOrDefaultAsync(cancellationToken);

        if (teamsheet is null)
        {
            return response;
        }

        var playerIds = teamsheet.Players.Select(player => player.PlayerId).ToList();
        var playerDictionary = await _dbContext.Players
            .AsNoTracking()
            .Where(player => playerIds.Contains(player.Id))
            .ToDictionaryAsync(player => player.Id, cancellationToken);

        var playerEventDictionary = await _dbContext.PlayerEvents
            .AsNoTracking()
            .Where(playerEvent => playerEvent.FixtureId == request.FixtureId)
            .GroupBy(playerEvent => playerEvent.PlayerId)
            .ToDictionaryAsync(group => group.Key, cancellationToken);

        foreach (var teamsheetPlayer in teamsheet.Players)
        {
            if (!playerDictionary.TryGetValue(teamsheetPlayer.PlayerId, out var player))
            {
                continue;
            }

            playerEventDictionary.TryGetValue(teamsheetPlayer.PlayerId, out var playerEvents);

            response.Add(new PlayerFixtureStatisticsResponse(
                player.Id,
                player.Name,
                player.SquadNumber,
                player.Position,
                teamsheetPlayer.IsStarter,
                playerEvents?.Count(e => e.Type == PlayerEventType.PassSuccess) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.PassFailure) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.ShotOnTarget) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.ShotOffTarget) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.Goal) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.GoalAssist) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.OwnGoal) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.Save) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.Tackle) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.FoulWon) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.FoulConceded) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.YellowCard) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.RedCard) ?? 0));
        }

        return response;
    }
}
