using FluentValidation.TestHelper;
using SportsStatistics.Application.Fixtures.Update;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Tests.Fixtures.Update;

public class UpdateFixtureCommandValidatorTests
{
    private static readonly UpdateFixtureCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                   "Test Opponent",
                                                                   DateTime.UtcNow,
                                                                   Location.Home.Value);

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

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenLocationIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { LocationId = default };
        var expected = "'Fixture Location Name' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.LocationId)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldNotHaveAnyValidationErrors_WhenLocationIdIsValid()
    {
        foreach (var location in Location.List)
        {
            // Arrange.
            var command = BaseCommand with { LocationId = location.Value };

            // Act.
            var result = await _validator.TestValidateAsync(command);

            // Assert.
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenLocationIdIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { LocationId = -1 };
        var expected = $"Invalid fixture location.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.LocationId)
              .WithErrorMessage(expected);
    }
}
