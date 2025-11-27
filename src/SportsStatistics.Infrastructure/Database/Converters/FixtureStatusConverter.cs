using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class FixtureStatusConverter : ValueObjectConverter<Status, int>
{
    private FixtureStatusConverter() : base(type => type.Value, value => Status.Resolve(value).Value) { }
}
