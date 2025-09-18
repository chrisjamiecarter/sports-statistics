using SportsStatistics.Domain.Players;

namespace SportsStatistics.Web.Admin.Players;

internal static class PositionOptionDtoMapping
{
    public static PositionOptionDto ToDto(this Position position)
        => new(position.Id, position.Name);
}
