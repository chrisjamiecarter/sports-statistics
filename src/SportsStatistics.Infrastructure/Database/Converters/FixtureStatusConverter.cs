using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class FixtureStatusConverter : ValueObjectConverter<FixtureStatus>
{
    public static readonly FixtureStatusConverter Instance = new();

    private FixtureStatusConverter() : base(type => type.Name, value => FixtureStatus.FromName(value)) { }
}
