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
    public async Task ValidateAsync_ShouldNotHaveAnyValidationErrors_WhenCommandIsValid()
    {
        // Arrange.
        var command = BaseCommand;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenFixtureIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { FixtureId = default };
        var expected = "'Fixture Id' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.FixtureId)
              .WithErrorMessage(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenOpponentIsEmpty(string opponent)
    {
        // Arrange.
        var command = BaseCommand with { Opponent = opponent };
        var expected = "'Opponent' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Opponent)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenOpponentExceedsMaximumLength()
    {
        // Arrange.
        int max = 100;
        var command = BaseCommand with { Opponent = new string('a', max + 1) };
        var expected = $"The length of 'Opponent' must be {max} characters or fewer. You entered {command.Opponent.Length} characters.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Opponent)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenKickoffTimeUtcIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { KickoffTimeUtc = default };
        var expected = "'Kickoff Time Utc' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.KickoffTimeUtc)
              .WithErrorMessage(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenFixtureLocationNameIsEmpty(string fixtureLocationName)
    {
        // Arrange.
        var command = BaseCommand with { FixtureLocationName = fixtureLocationName };
        var expected = "'Fixture Location Name' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.FixtureLocationName)
              .WithErrorMessage(expected);
    }

    [Theory]
    [InlineData("Home")]
    [InlineData("Away")]
    [InlineData("Neutral")]
    public async Task ValidateAsync_ShouldNotHaveAnyValidationErrors_WhenFixtureLocationNameIsValid(string fixtureLocationName)
    {
        // Arrange.
        var command = BaseCommand with { FixtureLocationName = fixtureLocationName };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenFixtureLocationNameIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { FixtureLocationName = "The Moon" };
        var expected = $"Invalid fixture location. Valid fixture locations: {string.Join(", ", FixtureLocation.All)}.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.FixtureLocationName)
              .WithErrorMessage(expected);
    }
}
