using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class FixtureLocationConverter : ValueObjectConverter<FixtureLocation>
{
    public static readonly FixtureLocationConverter Instance = new();

    private FixtureLocationConverter() : base(type => type.Name, value => FixtureLocation.FromName(value)) { }
}
