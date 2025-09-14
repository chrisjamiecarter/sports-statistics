using SportsStatistics.Domain.Players;

namespace SportsStatistics.Web.Players;

internal static class PositionOptionDtoMappings
{
    public static PositionOptionDto ToDto(this Position position)
        => new(position.Id, position.Name);
}
