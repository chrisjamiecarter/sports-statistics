using Microsoft.FluentUI.AspNetCore.Components.Extensions;

namespace SportsStatistics.Web.Players;

public static class PlayerInputModelExtensions
{
    public static PlayerInputModel ToInputModel(this PlayerDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        return new()
        {
            Name = dto.Name,
            SquadNumber = dto.SquadNumber,
            Nationality = dto.Nationality,
            DateOfBirth = dto.DateOfBirth.ToDateTime(),
            Position = dto.Position,
        };
    }
}
