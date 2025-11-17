using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;
using SportsStatistics.Infrastructure.Seasons;

namespace SportsStatistics.Infrastructure.Tests.Seasons;

public class SeasonConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public SeasonConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new SeasonConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(Season))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(Season)}' entity type.");
    }

    [Fact]
    public void SeasonConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.Seasons.Schema;
        var expectedTable = Schemas.Seasons.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void SeasonConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(Season.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void SeasonConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedValueConverter = Converters.EntityIdConverter;
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(Season.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.GetValueConverter().ShouldBe(expectedValueConverter);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void SeasonConfiguration_ShouldConfigureStartDatePropertyCorrectly()
    {
        // Arrange.
        var expectedColumnType = "date";
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Season.StartDate));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnType().ShouldBe(expectedColumnType);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void SeasonConfiguration_ShouldConfigureEndDatePropertyCorrectly()
    {
        // Arrange.
        var expectedColumnType = "date";
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Season.EndDate));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnType().ShouldBe(expectedColumnType);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void SeasonConfiguration_ShouldIgnoreName()
    {
        // Arrange.

        // Act.
        var property = _entity.FindProperty(nameof(Season.Name));

        // Assert.
        property.ShouldBeNull();
    }
}
