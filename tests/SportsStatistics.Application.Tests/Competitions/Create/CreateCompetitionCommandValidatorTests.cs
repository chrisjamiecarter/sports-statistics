using FluentValidation.TestHelper;
using SportsStatistics.Application.Competitions.Create;
using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Application.Tests.Competitions.Create;

public class CreateCompetitionCommandValidatorTests
{
    private static readonly CreateCompetitionCommand BaseCommand = new("Test Name",
                                                                       CompetitionType.League.Name);

    private readonly CreateCompetitionCommandValidator _validator;

    public CreateCompetitionCommandValidatorTests()
    {
        _validator = new CreateCompetitionCommandValidator();
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

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task Should_HaveValidationError_When_NameIsEmpty(string name)
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
    public async Task Should_HaveValidationError_When_NameExceedsMaximumLength()
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
    public async Task Should_HaveValidationError_When_CompetitionTypeIsEmpty(string competitionType)
    {
        // Arrange.
        var command = BaseCommand with { CompetitionType = competitionType };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.CompetitionType)
              .WithErrorMessage("'Competition Type' must not be empty.");
    }

    [Theory]
    [InlineData("League")]
    [InlineData("Cup")]
    public async Task Should_NotHaveValidationError_When_CompetitionTypeIsValid(string competitionType)
    {
        // Arrange.
        var command = BaseCommand with { CompetitionType = competitionType };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_HaveValidationError_When_CompetitionTypeIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { CompetitionType = "Training" };
        var expectedMessage = $"Invalid competition type. Valid competition types: {string.Join(", ", CompetitionType.All)}.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.CompetitionType)
              .WithErrorMessage(expectedMessage);
    }
}
