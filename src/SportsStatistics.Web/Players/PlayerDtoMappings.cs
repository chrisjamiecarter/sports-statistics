using SportsStatistics.Application.Players.GetAll;

namespace SportsStatistics.Web.Players;

internal static class PlayerDtoMappings
{
    public static PlayerDto ToDto(this PlayerResponse player)
        => new(player.Id,
               player.Name,
               player.SquadNumber,
               player.Nationality,
               player.DateOfBirth,
               player.Position,
               player.Age);

    public static IQueryable<PlayerDto> ToQueryable(this List<PlayerResponse> players)
    {
        return players.Select(ToDto).AsQueryable();
    }
}
