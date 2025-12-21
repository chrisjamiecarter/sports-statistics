using SportsStatistics.Application.Players.GetAll;

namespace SportsStatistics.Web.MatchTracker;

internal static class PlayerMapper
{
    public static PlayerDto ToDto(this PlayerResponse player)
        => new(player.Id,
               player.Name,
               player.SquadNumber,
               player.Nationality,
               player.DateOfBirth,
               player.PositionId,
               player.Position,
               player.Age);

    //public static PlayerFormModel ToInputModel(this PlayerDto player, IEnumerable<PositionOptionDto> positionOptions)
    //    => new()
    //    {
    //        Name = player.Name,
    //        SquadNumber = player.SquadNumber,
    //        Nationality = player.Nationality,
    //        DateOfBirth = player.DateOfBirth.ToDateTime(),
    //        Position = positionOptions.SingleOrDefault(option => option.Value == player.PostionId),
    //    };

    //public static IQueryable<PlayerDto> ToQueryable(this List<PlayerResponse> players)
    //    => players.Select(ToDto).AsQueryable();
}
