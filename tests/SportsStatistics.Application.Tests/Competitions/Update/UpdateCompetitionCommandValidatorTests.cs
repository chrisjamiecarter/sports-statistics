using FluentValidation.TestHelper;
using SportsStatistics.Application.Competitions.Update;
using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Application.Tests.Competitions.Update;

public class UpdateCompetitionCommandValidatorTests
{
    private static readonly UpdateCompetitionCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                       "Test Name",
                                                                       Format.League.Value);

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
        var expected = "'Competition Id' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.CompetitionId)
              .WithErrorMessage(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenNameIsEmpty(string name)
    {
        // Arrange.
        var command = BaseCommand with { Name = name };
        var expected = "'Name' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenNameExceedsMaximumLength()
    {
        // Arrange.
        int max = 50;
        var name = new string('a', max + 1);
        var command = BaseCommand with { Name = name };
        var expected = $"The length of 'Name' must be {max} characters or fewer. You entered {name.Length} characters.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenFormatIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { FormatId = default };
        var expected = "'Competition Type Name' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.FormatId)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldNotHaveAnyValidationErrors_WhenFormatIdIsValid()
    {
        foreach (var format in Format.List)
        {
            // Arrange.
            var command = BaseCommand with { FormatId = format.Value };

            // Act.
            var result = await _validator.TestValidateAsync(command);

            // Assert.
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenFormatIdIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { FormatId = -1 };
        var expected = $"'Competition Type Name' is invalid.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.FormatId)
              .WithErrorMessage(expected);
    }
}
