using SportsStatistics.Domain.Entities;

namespace SportsStatistics.Application.Players.GetAll;

internal static class PlayerResponseMappings
{
    public static List<PlayerResponse> ToResponse(this IEnumerable<Player> players)
        => [.. players.Select(ToResponse)];

    public static PlayerResponse ToResponse(this Player player)
        => new(player.Id,
               player.Name,
               player.Role,
               player.SquadNumber,
               player.Nationality,
               player.DateOfBirth,
               player.Age);
}
