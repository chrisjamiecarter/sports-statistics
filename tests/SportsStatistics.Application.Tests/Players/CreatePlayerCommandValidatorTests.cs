using System.Text;
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
    public async Task CreatePlayerCommandValidator_Should_NotHaveValidationError()
    {
        // Arrange.
        var command = BaseCommand;

        // Act.
        var result = await _validator.ValidateAsync(command);

        // Assert.
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task CreatePlayerCommandValidator_Should_HaveValidationError_Name_WhenEmpty(string name)
    {
        // Arrange.
        var command = BaseCommand with { Name = name };

        // Act.
        var result = await _validator.ValidateAsync(command);

        // Assert.
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors[0].ShouldNotBeNull();
        result.Errors[0].ErrorMessage.ShouldBe("'Name' must not be empty.");
    }

    [Fact]
    public async Task CreatePlayerCommandValidator_Should_HaveValidationError_Name_WhenExceedsMaximumLength()
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
        var result = await _validator.ValidateAsync(command);

        // Assert.
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors[0].ShouldNotBeNull();
        result.Errors[0].ErrorMessage.ShouldBe(expectedMessage);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(100)]
    public async Task CreatePlayerCommandValidator_Should_HaveValidationError_SquadNumber_OutsideRange(int squadNumber)
    {
        // Arrange.
        var command = BaseCommand with { SquadNumber = squadNumber };
        var expectedMessage = $"'Squad Number' must be between 1 and 99. You entered {squadNumber}.";

        // Act.
        var result = await _validator.ValidateAsync(command);

        // Assert.
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors[0].ShouldNotBeNull();
        result.Errors[0].ErrorMessage.ShouldBe(expectedMessage);
    }

    [Fact]
    public async Task CreatePlayerCommandValidator_Should_HaveValidationError_SquadNumber_Taken()
    {
        // Arrange.
        var command = BaseCommand;
        SetupIsSquadNumberAvailableAsync(false);

        // Act.
        var result = await _validator.ValidateAsync(command);

        // Assert.
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors[0].ShouldNotBeNull();
        result.Errors[0].ErrorMessage.ShouldBe("Squad number is already taken by another player.");
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task CreatePlayerCommandValidator_Should_HaveValidationError_Nationality_WhenEmpty(string nationality)
    {
        // Arrange.
        var command = BaseCommand with { Nationality = nationality };

        // Act.
        var result = await _validator.ValidateAsync(command);

        // Assert.
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors[0].ShouldNotBeNull();
        result.Errors[0].ErrorMessage.ShouldBe("'Nationality' must not be empty.");
    }

    [Fact]
    public async Task CreatePlayerCommandValidator_Should_HaveValidationError_Nationality_WhenExceedsMaximumLength()
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
        var result = await _validator.ValidateAsync(command);

        // Assert.
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors[0].ShouldNotBeNull();
        result.Errors[0].ErrorMessage.ShouldBe(expectedMessage);
    }

    [Fact]
    public async Task CreatePlayerCommandValidator_Should_HaveValidationError_DateOfBirth_WhenEmpty()
    {
        // Arrange.
        var command = BaseCommand with { DateOfBirth = default };

        // Act.
        var result = await _validator.ValidateAsync(command);

        // Assert.
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors[0].ShouldNotBeNull();
        result.Errors[0].ErrorMessage.ShouldBe("'Date Of Birth' must not be empty.");
    }

    [Fact]
    public async Task CreatePlayerCommandValidator_Should_HaveValidationError_DateOfBirth_WhenLessThanFifteenYearsOld()
    {
        // Arrange.
        var dob = DateOnly.FromDateTime(DateTime.Today).AddYears(-10);
        var command = BaseCommand with { DateOfBirth = dob };

        // Act.
        var result = await _validator.ValidateAsync(command);

        // Assert.
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors[0].ShouldNotBeNull();
        result.Errors[0].ErrorMessage.ShouldBe("Player must be at least 15 years old.");
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task CreatePlayerCommandValidator_Should_HaveValidationError_Position_WhenEmpty(string position)
    {
        // Arrange.
        var command = BaseCommand with { Position = position };

        // Act.
        var result = await _validator.ValidateAsync(command);

        // Assert.
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBeGreaterThan(0);
        result.Errors[0].ShouldNotBeNull();
        result.Errors[0].ErrorMessage.ShouldBe("'Position' must not be empty.");
    }

    [Theory]
    [InlineData("Goalkeeper")]
    [InlineData("Defender")]
    [InlineData("Midfielder")]
    [InlineData("Attacker")]
    public async Task CreatePlayerCommandValidator_Should_NotHaveValidationError_Position(string position)
    {
        // Arrange.
        var command = BaseCommand with { Position = position };

        // Act.
        var result = await _validator.ValidateAsync(command);

        // Assert.
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Fact]
    public async Task CreatePlayerCommandValidator_Should_HaveValidationError_Position_WhenInvalid()
    {
        // Arrange.
        var command = BaseCommand with { Position = "Airline Pilot" };

        // Act.
        var result = await _validator.ValidateAsync(command);

        // Assert.
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors[0].ShouldNotBeNull();
        result.Errors[0].ErrorMessage.ShouldBe("Invalid position. Valid positions: Goalkeeper, Defender, Midfielder, Attacker.");
    }

    private void SetupIsSquadNumberAvailableAsync(bool returnValue)
    {
        _repositoryMock.Setup(r => r.IsSquadNumberAvailableAsync(It.IsAny<int>(),
                                                                 It.IsAny<Guid?>(),
                                                                 It.IsAny<CancellationToken>()))
                       .ReturnsAsync(returnValue);
    }
}
