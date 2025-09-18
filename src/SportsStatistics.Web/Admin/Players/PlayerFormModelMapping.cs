using SportsStatistics.Application.Players.Create;
using SportsStatistics.Application.Players.Update;

namespace SportsStatistics.Web.Admin.Players;

internal static class PlayerFormModelMapping
{
    public static CreatePlayerCommand ToCommand(this PlayerFormModel player)
    {
        return new(player.Name,
                   player.SquadNumber,
                   player.Nationality,
                   DateOnly.FromDateTime(player.DateOfBirth.GetValueOrDefault()),
                   player.Position?.Name ?? string.Empty);
    }

    public static UpdatePlayerCommand ToCommand(this PlayerFormModel player, Guid playerId)
    {
        return new(playerId,
                   player.Name,
                   player.SquadNumber,
                   player.Nationality,
                   DateOnly.FromDateTime(player.DateOfBirth.GetValueOrDefault()),
                   player.Position?.Name ?? string.Empty);
    }
}