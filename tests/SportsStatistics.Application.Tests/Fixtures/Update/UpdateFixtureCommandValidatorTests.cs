using FluentValidation.TestHelper;
using SportsStatistics.Application.Fixtures.Update;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Tests.Fixtures.Update;

public class UpdateFixtureCommandValidatorTests
{
    private static readonly UpdateFixtureCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                   DateTime.UtcNow.AddDays(7),
                                                                   FixtureLocation.Home.Name,
                                                                   0,
                                                                   0,
                                                                   FixtureStatus.Scheduled.Name);

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
    public async Task Should_HaveValidationError_When_LocationNameIsEmpty(string locationName)
    {
        // Arrange.
        var command = BaseCommand with { LocationName = locationName };
        var expectedErrorMessage = "'Location Name' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.LocationName)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("Home")]
    [InlineData("Away")]
    [InlineData("Neutral")]
    public async Task Should_NotHaveValidationError_When_LocationNameIsValid(string locationName)
    {
        // Arrange.
        var command = BaseCommand with { LocationName = locationName };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_HaveValidationError_When_LocationNameIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { LocationName = "The Moon" };
        var expectedMessage = $"Invalid fixture location. Valid fixture locations: {string.Join(", ", FixtureLocation.All)}.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.LocationName)
              .WithErrorMessage(expectedMessage);
    }

    [Fact]
    public async Task Should_NotHaveValidationError_When_HomeGoalsIsGreaterThanZero()
    {
        // Arrange.
        var command = BaseCommand with { HomeGoals = 1 };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_HaveValidationError_When_HomeGoalsIsLessThanZero()
    {
        // Arrange.
        var command = BaseCommand with { HomeGoals = -1 };
        var expectedErrorMessage = "'Home Goals' must be greater than or equal to '0'.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.HomeGoals)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Fact]
    public async Task Should_NotHaveValidationError_When_AwayGoalsIsGreaterThanZero()
    {
        // Arrange.
        var command = BaseCommand with { AwayGoals = 1 };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_HaveValidationError_When_AwayGoalsIsLessThanZero()
    {
        // Arrange.
        var command = BaseCommand with { AwayGoals = -1 };
        var expectedErrorMessage = "'Away Goals' must be greater than or equal to '0'.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.AwayGoals)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task Should_HaveValidationError_When_StatusNameIsEmpty(string statusName)
    {
        // Arrange.
        var command = BaseCommand with { StatusName = statusName };
        var expectedErrorMessage = "'Status Name' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.StatusName)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("Scheduled")]
    [InlineData("Completed")]
    [InlineData("Postponed")]
    [InlineData("Cancelled")]
    public async Task Should_NotHaveValidationError_When_StatusNameIsValid(string statusName)
    {
        // Arrange.
        var command = BaseCommand with { StatusName = statusName };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_HaveValidationError_When_StatusNameIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { StatusName = "TBC" };
        var expectedMessage = $"Invalid fixture status. Valid fixture statuses: {string.Join(", ", FixtureStatus.All)}.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.StatusName)
              .WithErrorMessage(expectedMessage);
    }
}
