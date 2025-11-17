using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;
using SportsStatistics.Infrastructure.Fixtures;

namespace SportsStatistics.Infrastructure.Tests.Fixtures;

public class FixtureConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public FixtureConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new FixtureConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(Fixture))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(Fixture)}' entity type.");
    }

    [Fact]
    public void FixtureConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.Fixtures.Schema;
        var expectedTable = Schemas.Fixtures.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(Fixture.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedValueConverter = Converters.EntityIdConverter;
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(Fixture.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.GetValueConverter().ShouldBe(expectedValueConverter);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureCompetitionIdPropertyCorrectly()
    {
        // Arrange.
        var expectedValueConverter = Converters.EntityIdConverter;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Fixture.CompetitionId));

        // Assert.
        property.ShouldNotBeNull();
        property.GetValueConverter().ShouldBe(expectedValueConverter);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureOpponentPropertyCorrectly()
    {
        // Arrange.
        var expectedMaxLength = 100;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Fixture.Opponent));

        // Assert.
        property.ShouldNotBeNull();
        property.GetMaxLength().ShouldBe(expectedMaxLength);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureKickoffTimeUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Fixture.KickoffTimeUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureLocationPropertyCorrectly()
    {
        // Arrange.
        var expectedValueConverter = Converters.FixtureLocationConverter;
        var expectedMaxLength = FixtureLocation.MaxLength;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Fixture.Location));

        // Assert.
        property.ShouldNotBeNull();
        property.GetValueConverter().ShouldBe(expectedValueConverter);
        property.GetMaxLength().ShouldBe(expectedMaxLength);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureScorePropertyCorrectly()
    {
        // Arrange.
        var expectedIsOwned = true;
        var expectedIsNullable = false;

        // Act.
        var ownedNavigation = _entity.FindNavigation(nameof(Fixture.Score));
        var ownedEntityType = ownedNavigation?.TargetEntityType;
        var homeGoalsProperty = ownedEntityType?.FindProperty(nameof(FixtureScore.HomeGoals));
        var awayGoalsProperty = ownedEntityType?.FindProperty(nameof(FixtureScore.AwayGoals));

        // Assert.
        ownedEntityType.ShouldNotBeNull();
        ownedEntityType.IsOwned().ShouldBe(expectedIsOwned);
        homeGoalsProperty.ShouldNotBeNull();
        homeGoalsProperty.IsNullable.ShouldBe(expectedIsNullable);
        awayGoalsProperty.ShouldNotBeNull();
        awayGoalsProperty.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureStatusPropertyCorrectly()
    {
        // Arrange.
        var expectedValueConverter = Converters.FixtureStatusConverter;
        var expectedMaxLength = FixtureStatus.MaxLength;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Fixture.Status));

        // Assert.
        property.ShouldNotBeNull();
        property.GetValueConverter().ShouldBe(expectedValueConverter);
        property.GetMaxLength().ShouldBe(expectedMaxLength);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureCompetitionIdAsForeignKey()
    {
        // Arrange.
        var expected = nameof(Fixture.CompetitionId);

        // Act.
        var foreignKey = _entity.GetForeignKeys()
                                .SingleOrDefault(fk => fk.Properties.Any(p => p.Name == expected));

        // Assert.
        foreignKey.ShouldNotBeNull();
        foreignKey.Properties.ShouldHaveSingleItem();
        foreignKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }
}
