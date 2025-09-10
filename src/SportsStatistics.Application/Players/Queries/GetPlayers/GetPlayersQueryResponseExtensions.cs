using SportsStatistics.Domain.Entities;

namespace SportsStatistics.Application.Players.Queries.GetPlayers;

internal static class GetPlayersQueryResponseExtensions
{
    public static List<GetPlayersQueryResponse> ToResponse(this IEnumerable<Player> players) 
        => [.. players.Select(ToResponse)];

    public static GetPlayersQueryResponse ToResponse(this Player player) 
        => new(player.Id, player.Name, player.Role, player.SquadNumber, player.Nationality, player.Age);
}