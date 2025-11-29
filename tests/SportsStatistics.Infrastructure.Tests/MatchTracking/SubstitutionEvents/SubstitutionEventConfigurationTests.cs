using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;
using SportsStatistics.Infrastructure.MatchTracking.SubstitutionEvents;

namespace SportsStatistics.Infrastructure.Tests.MatchTracking.SubstitutionEvents;

public class SubstitutionEventConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public SubstitutionEventConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new SubstitutionEventConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(SubstitutionEvent))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(SubstitutionEvent)}' entity type.");
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.SubstitutionEvents.Schema;
        var expectedTable = Schemas.SubstitutionEvents.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(SubstitutionEvent.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(SubstitutionEvent.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigureFixtureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(SubstitutionEvent.FixtureId));

        // Assert.
        property.ShouldNotBeNull();
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigureMinutePropertyCorrectly()
    {
        // Arrange.
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(SubstitutionEvent.Minute));

        // Assert.
        property.ShouldNotBeNull();
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigureOccurredAtUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(SubstitutionEvent.OccurredAtUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    //[Fact]
    //public void SubstitutionEventConfiguration_ShouldConfigurePlayerOutIdPropertyCorrectly()
    //{
    //    // Arrange.
    //    var expectedIsNullable = false;

    //    // Act.
    //    var property = _entity.FindProperty(nameof(SubstitutionEvent.PlayerOutId));

    //    // Assert.
    //    property.ShouldNotBeNull();
    //    property.IsNullable.ShouldBe(expectedIsNullable);
    //}

    //[Fact]
    //public void SubstitutionEventConfiguration_ShouldConfigurePlayerInIdPropertyCorrectly()
    //{
    //    // Arrange.
    //    var expectedValueConverter = Converters.EntityIdConverter;
    //    var expectedIsNullable = false;

    //    // Act.
    //    var property = _entity.FindProperty(nameof(SubstitutionEvent.PlayerInId));

    //    // Assert.
    //    property.ShouldNotBeNull();
    //    property.GetValueConverter().ShouldBe(expectedValueConverter);
    //    property.IsNullable.ShouldBe(expectedIsNullable);
    //}

    //[Fact]
    //public void SubstitutionEventConfiguration_ShouldConfigurePlayerOutIdAsForeignKey()
    //{
    //    // Arrange.
    //    var expected = nameof(SubstitutionEvent.PlayerOutId);

    //    // Act.
    //    var foreignKey = _entity.GetForeignKeys()
    //                            .SingleOrDefault(fk => fk.Properties.Any(p => p.Name == expected));

    //    // Assert.
    //    foreignKey.ShouldNotBeNull();
    //    foreignKey.Properties.ShouldHaveSingleItem();
    //    foreignKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    //}

    //[Fact]
    //public void SubstitutionEventConfiguration_ShouldConfigurePlayerInIdAsForeignKey()
    //{
    //    // Arrange.
    //    var expected = nameof(SubstitutionEvent.PlayerInId);

    //    // Act.
    //    var foreignKey = _entity.GetForeignKeys()
    //                            .SingleOrDefault(fk => fk.Properties.Any(p => p.Name == expected));

    //    // Assert.
    //    foreignKey.ShouldNotBeNull();
    //    foreignKey.Properties.ShouldHaveSingleItem();
    //    foreignKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    //}
}
