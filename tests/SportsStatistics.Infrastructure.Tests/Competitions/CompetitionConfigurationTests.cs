using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Infrastructure.Competitions;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;

namespace SportsStatistics.Infrastructure.Tests.Competitions;

public class CompetitionConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public CompetitionConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new CompetitionConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(Competition))
            ?? throw new InvalidOperationException("Unable to find 'Competition' entity type.");
    }

    [Fact]
    public void CompetitionConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.Competitions.Schema;
        var expectedTable = Schemas.Competitions.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(Competition.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedValueConverter = Converters.EntityIdConverter;
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(Competition.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.GetValueConverter().ShouldBe(expectedValueConverter);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureSeasonIdPropertyCorrectly()
    {
        // Arrange.
        var expectedValueConverter = Converters.EntityIdConverter;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Competition.SeasonId));

        // Assert.
        property.ShouldNotBeNull();
        property.GetValueConverter().ShouldBe(expectedValueConverter);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureNamePropertyCorrectly()
    {
        // Arrange.
        var expectedMaxLength = 50;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Competition.Name));

        // Assert.
        property.ShouldNotBeNull();
        property.GetMaxLength().ShouldBe(expectedMaxLength);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureTypePropertyCorrectly()
    {
        // Arrange.
        var expectedValueConverter = Converters.CompetitionTypeConverter;
        var expectedMaxLength = CompetitionType.MaxLength;

        // Act.
        var property = _entity.FindProperty(nameof(Competition.Type));

        // Assert.
        property.ShouldNotBeNull();
        property.GetValueConverter().ShouldBe(expectedValueConverter);
        property.GetMaxLength().ShouldBe(expectedMaxLength);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureSeasonIdForeignKey()
    {
        // Arrange.
        var expected = nameof(Competition.SeasonId);

        // Act.
        var foreignKey = _entity.GetForeignKeys()
                                .SingleOrDefault(fk => fk.Properties.Any(p => p.Name == expected));

        // Assert.
        foreignKey.ShouldNotBeNull();
        foreignKey.Properties.ShouldHaveSingleItem();
        foreignKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }
}
