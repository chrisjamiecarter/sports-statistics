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

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Id)
              .WithErrorMessage("'Id' must not be empty.");
    }

    [Fact]
    public async Task Should_HaveValidationError_When_IdIsNotVersion7()
    {
        // Arrange.
        var command = BaseCommand with { Id = Guid.NewGuid() };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Id)
              .WithErrorMessage("'Id' is not in the correct format.");
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
    public async Task Should_HaveValidationError_When_CompetitionTypeIsEmpty(string competitionTypeName)
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
    public async Task Should_NotHaveValidationError_When_CompetitionTypeIsValid(string competitionType)
    {
        // Arrange.
        var command = BaseCommand with { CompetitionTypeName = competitionType };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_HaveValidationError_When_CompetitionTypeIsInvalid()
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
