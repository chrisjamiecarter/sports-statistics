using FluentValidation.TestHelper;
using SportsStatistics.Application.Competitions.Create;
using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Application.Tests.Competitions.Create;

public class CreateCompetitionCommandValidatorTests
{
    private static readonly CreateCompetitionCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                       "Test Name",
                                                                       CompetitionType.League.Name);

    private readonly CreateCompetitionCommandValidator _validator;

    public CreateCompetitionCommandValidatorTests()
    {
        _validator = new CreateCompetitionCommandValidator();
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
    public async Task ValidateAsync_ShouldHaveValidationError_WhenSeasonIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { SeasonId = default };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorMessage("'Season Id' must not be empty.");
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenNameIsEmpty(string name)
    {
        // Arrange.
        var command = BaseCommand with { Name = name };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorMessage("'Name' must not be empty.");
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenNameExceedsMaximumLength()
    {
        // Arrange.
        int max = 50;
        var name = new string('a', max + 1);
        var command = BaseCommand with { Name = name };
        var expectedMessage = $"The length of 'Name' must be {max} characters or fewer. You entered {name.Length} characters.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorMessage(expectedMessage);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenCompetitionTypeNameIsEmpty(string competitionTypeName)
    {
        // Arrange.
        var command = BaseCommand with { CompetitionTypeName = competitionTypeName };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.CompetitionTypeName)
              .WithErrorMessage("'Competition Type Name' must not be empty.");
    }

    [Theory]
    [InlineData("League")]
    [InlineData("Cup")]
    public async Task ValidateAsync_ShouldNotHaveAnyValidationErrors_WhenCompetitionTypeNameIsValid(string competitionTypeName)
    {
        // Arrange.
        var command = BaseCommand with { CompetitionTypeName = competitionTypeName };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenCompetitionTypeNameIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { CompetitionTypeName = "Training" };
        var expectedMessage = $"'Competition Type Name' is invalid. Valid options: {string.Join(", ", CompetitionType.All)}.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.CompetitionTypeName)
              .WithErrorMessage(expectedMessage);
    }
}
