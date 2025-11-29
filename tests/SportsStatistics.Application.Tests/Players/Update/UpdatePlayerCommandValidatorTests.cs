using FluentValidation.TestHelper;
using SportsStatistics.Application.Players.Update;
using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Tests.Players.Update;

public class UpdatePlayerCommandValidatorTests
{
    private static readonly UpdatePlayerCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                  "Test Name",
                                                                  1,
                                                                  "Test Nationality",
                                                                  DateOnly.FromDateTime(DateTime.Today).AddYears(-15),
                                                                  Position.Goalkeeper.Value);

    private readonly UpdatePlayerCommandValidator _validator;

    public UpdatePlayerCommandValidatorTests()
    {
        _validator = new UpdatePlayerCommandValidator();
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
    public async Task ValidateAsync_ShouldHaveValidationError_WhenPlayerIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { PlayerId = default };
        var expected = "'Player Id' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.PlayerId)
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
        int max = 100;
        var command = BaseCommand with { Name = new string('a', max + 1) };
        var expected = $"The length of 'Name' must be {max} characters or fewer. You entered {command.Name.Length} characters.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorMessage(expected);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(100)]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenSquadNumberIsOutsideRange(int squadNumber)
    {
        // Arrange.
        var command = BaseCommand with { SquadNumber = squadNumber };
        var expected = $"'Squad Number' must be between 1 and 99. You entered {squadNumber}.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.SquadNumber)
              .WithErrorMessage(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenNationalityIsEmpty(string nationality)
    {
        // Arrange.
        var command = BaseCommand with { Nationality = nationality };
        var expected = "'Nationality' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Nationality)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenNationalityExceedsMaximumLength()
    {
        // Arrange.
        int max = 100;
        var command = BaseCommand with { Nationality = new string('a', max + 1) };
        var expected = $"The length of 'Nationality' must be 100 characters or fewer. You entered {command.Nationality.Length} characters.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Nationality)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenDateOfBirthIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { DateOfBirth = default };
        var expected = "'Date Of Birth' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.DateOfBirth)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenDateOfBirthIsLessThanFifteenYearsAgo()
    {
        // Arrange.
        var command = BaseCommand with { DateOfBirth = DateOnly.FromDateTime(DateTime.Today).AddYears(-10) };
        var expected = "Player must be at least 15 years old.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.DateOfBirth)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenPositionIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { PositionId = default };
        var expected = "'Position Name' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.PositionId)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldNotHaveAnyValidationErrors_WhenPositionIdIsValid()
    {
        // Arrange.
        var command = BaseCommand with { PositionId = Position.Goalkeeper.Value };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenPositionIdIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { PositionId = -1 };
        var expected = $"Invalid position.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.PositionId)
              .WithErrorMessage(expected);
    }
}
