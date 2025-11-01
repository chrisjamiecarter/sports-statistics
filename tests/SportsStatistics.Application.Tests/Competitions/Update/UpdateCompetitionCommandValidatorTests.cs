using FluentValidation.TestHelper;
using SportsStatistics.Application.Competitions.Update;
using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Application.Tests.Competitions.Update;

public class UpdateCompetitionCommandValidatorTests
{
    private static readonly UpdateCompetitionCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                       "Test Name",
                                                                       CompetitionType.League.Name);

    private readonly UpdateCompetitionCommandValidator _validator;

    public UpdateCompetitionCommandValidatorTests()
    {
        _validator = new UpdateCompetitionCommandValidator();
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
    public async Task ValidateAsync_ShouldHaveValidationError_WhenIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { CompetitionId = default };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.CompetitionId)
              .WithErrorMessage("'Competition Id' must not be empty.");
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
