using System.Text;
using FluentValidation.TestHelper;
using SportsStatistics.Application.Players;
using SportsStatistics.Application.Players.Create;
using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Tests.Players;

public class CreatePlayerCommandValidatorTests
{
    private static readonly CreatePlayerCommand BaseCommand = new("Test Name", 1, "Test Nationality", DateOnly.FromDateTime(DateTime.Today).AddYears(-15), Position.Goalkeeper.Name);

    private readonly Mock<IPlayerRepository> _repositoryMock;
    private readonly CreatePlayerCommandValidator _validator;

    public CreatePlayerCommandValidatorTests()
    {
        _repositoryMock = new Mock<IPlayerRepository>();
        SetupIsSquadNumberAvailableAsync(true);

        _validator = new CreatePlayerCommandValidator(_repositoryMock.Object);
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
        var sb = new StringBuilder();
        while (sb.Length <= 100)
        {
            sb.Append("Test");
        }
        var command = BaseCommand with { Name = sb.ToString() };
        var expectedMessage = $"The length of 'Name' must be 100 characters or fewer. You entered {sb.Length} characters.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorMessage(expectedMessage);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(100)]
    public async Task Should_HaveValidationError_When_SquadNumberIsOutsideRange(int squadNumber)
    {
        // Arrange.
        var command = BaseCommand with { SquadNumber = squadNumber };
        var expectedMessage = $"'Squad Number' must be between 1 and 99. You entered {squadNumber}.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.SquadNumber)
              .WithErrorMessage(expectedMessage);
    }

    [Fact]
    public async Task Should_HaveValidationError_When_SquadNumberIsUnavailable()
    {
        // Arrange.
        var command = BaseCommand;
        SetupIsSquadNumberAvailableAsync(false);

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.SquadNumber)
              .WithErrorMessage("Squad number is already taken by another player.");
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task Should_HaveValidationError_When_NationalityIsEmpty(string nationality)
    {
        // Arrange.
        var command = BaseCommand with { Nationality = nationality };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Nationality)
              .WithErrorMessage("'Nationality' must not be empty.");
    }

    [Fact]
    public async Task Should_HaveValidationError_When_NationalityExceedsMaximumLength()
    {
        // Arrange.
        var sb = new StringBuilder();
        while (sb.Length <= 100)
        {
            sb.Append("Test");
        }
        var command = BaseCommand with { Nationality = sb.ToString() };
        var expectedMessage = $"The length of 'Nationality' must be 100 characters or fewer. You entered {sb.Length} characters.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Nationality)
              .WithErrorMessage(expectedMessage);
    }

    [Fact]
    public async Task Should_HaveValidationError_When_DateOfBirthIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { DateOfBirth = default };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.DateOfBirth)
              .WithErrorMessage("'Date Of Birth' must not be empty.");
    }

    [Fact]
    public async Task Should_HaveValidationError_When_DateOfBirthIsLessThanFifteenYearsAgo()
    {
        // Arrange.
        var dob = DateOnly.FromDateTime(DateTime.Today).AddYears(-10);
        var command = BaseCommand with { DateOfBirth = dob };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.DateOfBirth)
              .WithErrorMessage("Player must be at least 15 years old.");
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task Should_HaveValidationError_When_PositionIsEmpty(string position)
    {
        // Arrange.
        var command = BaseCommand with { Position = position };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Position)
              .WithErrorMessage("'Position' must not be empty.");
    }

    [Theory]
    [InlineData("Goalkeeper")]
    [InlineData("Defender")]
    [InlineData("Midfielder")]
    [InlineData("Attacker")]
    public async Task Should_NotHaveValidationError_When_PositionIsValid(string position)
    {
        // Arrange.
        var command = BaseCommand with { Position = position };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Should_HaveValidationError_When_PositionIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { Position = "Airline Pilot" };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Position)
              .WithErrorMessage("Invalid position. Valid positions: Goalkeeper, Defender, Midfielder, Attacker.");
    }

    private void SetupIsSquadNumberAvailableAsync(bool returnValue)
    {
        _repositoryMock.Setup(r => r.IsSquadNumberAvailableAsync(It.IsAny<int>(),
                                                                 It.IsAny<Guid?>(),
                                                                 It.IsAny<CancellationToken>()))
                       .ReturnsAsync(returnValue);
    }
}
