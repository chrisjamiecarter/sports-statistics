using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;
using SportsStatistics.Infrastructure.MatchTracking.MatchEvents;

namespace SportsStatistics.Infrastructure.Tests.MatchTracking.MatchEvents;

public class MatchEventConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public MatchEventConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new MatchEventConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(MatchEvent))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(MatchEvent)}' entity type.");
    }

    [Fact]
    public void MatchEventConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.MatchEvents.Schema;
        var expectedTable = Schemas.MatchEvents.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(MatchEvent.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(MatchEvent.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureFixtureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(MatchEvent.FixtureId));

        // Assert.
        property.ShouldNotBeNull();
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureMinutePropertyCorrectly()
    {
        // Arrange.
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(MatchEvent.Minute));

        // Assert.
        property.ShouldNotBeNull();
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureOccurredAtUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(MatchEvent.OccurredAtUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureTypePropertyCorrectly()
    {
        // Arrange.
        //var expectedValueConverter = Converters.MatchEventTypeConverter;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(MatchEvent.Type));

        // Assert.
        property.ShouldNotBeNull();
        //property.GetValueConverter().ShouldBe(expectedValueConverter);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureFixtureIdAsForeignKey()
    {
        // Arrange.
        var expected = nameof(MatchEvent.FixtureId);

        // Act.
        var foreignKey = _entity.GetForeignKeys()
                                .SingleOrDefault(fk => fk.Properties.Any(p => p.Name == expected));

        // Assert.
        foreignKey.ShouldNotBeNull();
        foreignKey.Properties.ShouldHaveSingleItem();
        foreignKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }
}
