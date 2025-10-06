using System.Linq.Expressions;
using System.Text;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Players;
using SportsStatistics.Application.Players.Create;
using SportsStatistics.Application.Players.Update;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Players.Update;

public class UpdatePlayerCommandValidatorTests
{
    private static readonly UpdatePlayerCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                  "Test Name",
                                                                  1,
                                                                  "Test Nationality",
                                                                  DateOnly.FromDateTime(DateTime.Today).AddYears(-15),
                                                                  Position.Goalkeeper.Name);

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
    public async Task ValidateAsync_ShouldHaveValidationError_WhenIdIsEmpty()
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

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenNameIsEmpty(string name)
    {
        // Arrange.
        var command = BaseCommand with { Name = name };
        var expectedErrorMessage = "'Name' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenNameExceedsMaximumLength()
    {
        // Arrange.
        int max = 100;
        var command = BaseCommand with { Name = new string('a', max + 1) };
        var expectedErrorMessage = $"The length of 'Name' must be {max} characters or fewer. You entered {command.Name.Length} characters.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(100)]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenSquadNumberIsOutsideRange(int squadNumber)
    {
        // Arrange.
        var command = BaseCommand with { SquadNumber = squadNumber };
        var expectedErrorMessage = $"'Squad Number' must be between 1 and 99. You entered {squadNumber}.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.SquadNumber)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task ValidateAsync_Should_HaveValidationError_When_NationalityIsEmpty(string nationality)
    {
        // Arrange.
        var command = BaseCommand with { Nationality = nationality };
        var expectedErrorMessage = "'Nationality' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Nationality)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Fact]
    public async Task ValidateAsync_Should_HaveValidationError_When_NationalityExceedsMaximumLength()
    {
        // Arrange.
        int max = 100;
        var command = BaseCommand with { Nationality = new string('a', max + 1) };
        var expectedErrorMessage = $"The length of 'Nationality' must be 100 characters or fewer. You entered {command.Nationality.Length} characters.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Nationality)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Fact]
    public async Task ValidateAsync_Should_HaveValidationError_When_DateOfBirthIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { DateOfBirth = default };
        var expectedErrorMessage = "'Date Of Birth' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.DateOfBirth)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Fact]
    public async Task ValidateAsync_Should_HaveValidationError_When_DateOfBirthIsLessThanFifteenYearsAgo()
    {
        // Arrange.
        var command = BaseCommand with { DateOfBirth = DateOnly.FromDateTime(DateTime.Today).AddYears(-10) };
        var expectedErrorMessage = "Player must be at least 15 years old.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.DateOfBirth)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenPositionNameIsEmpty(string positionName)
    {
        // Arrange.
        var command = BaseCommand with { PositionName = positionName };
        var expectedErrorMessage = "'Position Name' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.PositionName)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("Goalkeeper")]
    [InlineData("Defender")]
    [InlineData("Midfielder")]
    [InlineData("Attacker")]
    public async Task ValidateAsync_ShouldNotHaveAnyValidationErrors_WhenPositionNameIsValid(string positionName)
    {
        // Arrange.
        var command = BaseCommand with { PositionName = positionName };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenPositionNameIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { PositionName = "Airline Pilot" };
        var expectedErrorMessage = $"Invalid position. Valid positions: {string.Join(", ", Position.All)}.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.PositionName)
              .WithErrorMessage(expectedErrorMessage);
    }
}
