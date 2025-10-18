using FluentValidation.TestHelper;
using SportsStatistics.Application.Fixtures.Update;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Tests.Fixtures.Update;

public class UpdateFixtureCommandValidatorTests
{
    private static readonly UpdateFixtureCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                   "Test Opponent",
                                                                   DateTime.UtcNow.AddDays(7),
                                                                   FixtureLocation.Home.Name);

    private readonly UpdateFixtureCommandValidator _validator;

    public UpdateFixtureCommandValidatorTests()
    {
        _validator = new UpdateFixtureCommandValidator();
    }

    [Fact]
    public async Task Should_NotHaveValidationError_When_IsValid()
    {
        // Arrange.
        var command = BaseCommand;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_HaveValidationError_When_IdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { Id = default };
        var expectedErrorMessage = "'Id' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Id)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Fact]
    public async Task Should_HaveValidationError_When_IdIsNotVersion7()
    {
        // Arrange.
        var command = BaseCommand with { Id = Guid.NewGuid() };
        var expectedErrorMessage = "'Id' is not in the correct format.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Id)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Fact]
    public async Task Should_HaveValidationError_When_KickoffTimeUtcIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { KickoffTimeUtc = default };
        var expectedErrorMessage = "'Kickoff Time Utc' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.KickoffTimeUtc)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task Should_HaveValidationError_When_FixtureLocationNameIsEmpty(string fixtureLocationName)
    {
        // Arrange.
        var command = BaseCommand with { FixtureLocationName = fixtureLocationName };
        var expectedErrorMessage = "'Fixture Location Name' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.FixtureLocationName)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("Home")]
    [InlineData("Away")]
    [InlineData("Neutral")]
    public async Task Should_NotHaveValidationError_When_FixtureLocationNameIsValid(string fixtureLocationName)
    {
        // Arrange.
        var command = BaseCommand with { FixtureLocationName = fixtureLocationName };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_HaveValidationError_When_FixtureLocationNameIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { FixtureLocationName = "The Moon" };
        var expectedMessage = $"Invalid fixture location. Valid fixture locations: {string.Join(", ", FixtureLocation.All)}.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.FixtureLocationName)
              .WithErrorMessage(expectedMessage);
    }
}
