using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;
using SportsStatistics.Infrastructure.Players;

namespace SportsStatistics.Infrastructure.Tests.Players;

public class PlayerConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public PlayerConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new PlayerConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(Player))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(Player)}' entity type.");
    }

    [Fact]
    public void PlayerConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.Players.Schema;
        var expectedTable = Schemas.Players.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(Player.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(Player.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureNamePropertyCorrectly()
    {
        // Arrange.
        var expectedMaxLength = 100;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Player.Name));

        // Assert.
        property.ShouldNotBeNull();
        property.GetMaxLength().ShouldBe(expectedMaxLength);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureSquadNumberPropertyCorrectly()
    {
        // Arrange.
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Player.SquadNumber));

        // Assert.
        property.ShouldNotBeNull();
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureNationalityPropertyCorrectly()
    {
        // Arrange.
        var expectedMaxLength = 100;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Player.Nationality));

        // Assert.
        property.ShouldNotBeNull();
        property.GetMaxLength().ShouldBe(expectedMaxLength);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureDateOfBirthPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnType = "date";
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Player.DateOfBirth));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnType().ShouldBe(expectedColumnType);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigurePositionPropertyCorrectly()
    {
        // Arrange.
        //var expectedValueConverter = Converters.PlayerPositionConverter;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Player.Position));

        // Assert.
        property.ShouldNotBeNull();
        //property.GetValueConverter().ShouldBe(expectedValueConverter);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerConfiguration_ShouldIgnoreAge()
    {
        // Arrange.

        // Act.
        var property = _entity.FindProperty(nameof(Player.Age));

        // Assert.
        property.ShouldBeNull();
    }
}
