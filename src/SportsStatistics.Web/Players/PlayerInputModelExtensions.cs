using static SportsStatistics.Web.Players.PlayerForm;

namespace SportsStatistics.Web.Players;

public static class PlayerInputModelExtensions
{
    public static PlayerInputModel ToInputModel(this PlayerDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        return new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Role = dto.Role,
            SquadNumber = dto.SquadNumber,
            Nationality = dto.Nationality
        };
    }
}
