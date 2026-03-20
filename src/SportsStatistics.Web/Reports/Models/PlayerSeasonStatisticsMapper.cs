using SportsStatistics.Application.Reports.GetPlayerSeasonStatistics;

namespace SportsStatistics.Web.Reports.Models;

internal static class PlayerSeasonStatisticsMapper
{
    public static IQueryable<PlayerSeasonStatisticsDto> ToQueryable(this List<PlayerSeasonStatisticsResponse> statistics) => 
        statistics.Select(ToDto).AsQueryable();

    public static PlayerSeasonStatisticsDto ToDto(this PlayerSeasonStatisticsResponse statistic) => new(
        statistic.PlayerId,
        statistic.PlayerName,
        statistic.SquadNumber,
        statistic.Position,
        statistic.GamesStarted,
        statistic.GamesPlayed,
        statistic.PassSuccessCount,
        statistic.PassFailureCount,
        statistic.ShotOnTargetCount,
        statistic.ShotOffTargetCount,
        statistic.GoalCount,
        statistic.GoalAssistCount,
        statistic.OwnGoalCount,
        statistic.SaveCount,
        statistic.TackleCount,
        statistic.FoulWonCount,
        statistic.FoulConcededCount,
        statistic.YellowCardCount,
        statistic.RedCardCount);
}
