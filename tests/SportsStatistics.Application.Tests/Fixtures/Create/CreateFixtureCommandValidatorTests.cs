using FluentValidation.TestHelper;
using SportsStatistics.Application.Fixtures.Create;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Tests.Fixtures.Create;

public class CreateFixtureCommandValidatorTests
{
    private static readonly CreateFixtureCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                   "Test Opponent",
                                                                   DateTime.UtcNow,
                                                                   FixtureLocation.Home.Name);

    private readonly CreateFixtureCommandValidator _validator;

    public CreateFixtureCommandValidatorTests()
    {
        _validator = new CreateFixtureCommandValidator();
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

    [Fact]
    public async Task Should_HaveValidationError_When_CompetitionIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { CompetitionId = default };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.CompetitionId)
              .WithErrorMessage("'Competition Id' must not be empty.");
    }

    [Fact]
    public async Task Should_HaveValidationError_When_CompetitionIdIsNotVersion7()
    {
        // Arrange.
        var command = BaseCommand with { CompetitionId = Guid.NewGuid() };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.CompetitionId)
              .WithErrorMessage("'Competition Id' is not in the correct format.");
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task Should_HaveValidationError_When_FixtureLocationIsEmpty(string fixtureLocation)
    {
        // Arrange.
        var command = BaseCommand with { LocationName = fixtureLocation };
        var expectedErrorMessage = "'Fixture Location' must not be empty.";

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
    public async Task Should_NotHaveValidationError_When_FixtureLocationIsValid(string fixtureLocation)
    {
        // Arrange.
        var command = BaseCommand with { LocationName = fixtureLocation };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_HaveValidationError_When_FixtureLocationIsInvalid()
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
}
