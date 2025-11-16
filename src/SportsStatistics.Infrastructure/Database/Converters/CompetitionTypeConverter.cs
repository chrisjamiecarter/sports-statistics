using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class CompetitionTypeConverter : ValueObjectConverter<CompetitionType>
{
    public static readonly CompetitionTypeConverter Instance = new();

    private CompetitionTypeConverter() : base(type => type.Name, value => CompetitionType.FromName(value)) { }
}
