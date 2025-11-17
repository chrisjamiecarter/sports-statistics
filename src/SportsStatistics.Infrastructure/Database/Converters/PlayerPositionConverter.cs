using SportsStatistics.Domain.Players;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class PlayerPositionConverter : ValueObjectConverter<Position>
{
    public static readonly PlayerPositionConverter Instance = new();

    private PlayerPositionConverter() : base(type => type.Name, value => Position.FromName(value)) { }
}
