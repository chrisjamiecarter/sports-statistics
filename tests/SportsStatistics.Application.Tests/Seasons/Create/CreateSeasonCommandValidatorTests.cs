using FluentValidation.TestHelper;
using SportsStatistics.Application.Seasons;
using SportsStatistics.Application.Seasons.Create;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Seasons.Create;

public class CreateSeasonCommandValidatorTests
{
    private static readonly CreateSeasonCommand BaseCommand = new(new DateOnly(2025, 7, 1),
                                                                  new DateOnly(2026, 6, 30));

    private readonly Mock<ISeasonRepository> _repositoryMock;
    private readonly CreateSeasonCommandValidator _validator;

    public CreateSeasonCommandValidatorTests()
    {
        _repositoryMock = new Mock<ISeasonRepository>();
        SetupDoesDateOverlapExistingAsync(false);

        _validator = new CreateSeasonCommandValidator(_repositoryMock.Object);
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
    public async Task Should_HaveValidationError_When_StartDateIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { StartDate = default };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.StartDate)
              .WithErrorMessage("'Start Date' must not be empty.");
    }

    [Fact]
    public async Task Should_HaveValidationError_When_StartDateIsNotLessThanEndDate()
    {
        // Arrange.
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var endDate = startDate.AddDays(-1);
        var command = BaseCommand with
        {
            StartDate = startDate,
            EndDate = endDate
        };
        var expectedMessage = $"'Start Date' must be less than '{endDate:dd/MM/yyyy}'.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.StartDate)
              .WithErrorMessage(expectedMessage);
    }

    [Fact]
    public async Task Should_HaveValidationError_When_StartDateOverlapsWithExisting()
    {
        // Arrange.
        var command = BaseCommand;
        SetupDoesDateOverlapExistingAsync(true);

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.StartDate)
              .WithErrorMessage("'Start Date' overlaps with an existing season.");
    }


    [Fact]
    public async Task Should_HaveValidationError_When_EndDateIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { EndDate = default };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.EndDate)
              .WithErrorMessage("'End Date' must not be empty.");
    }

    [Fact]
    public async Task Should_HaveValidationError_When_EndDateIsNotGreaterThanStartDate()
    {
        // Arrange.
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var endDate = startDate.AddDays(-1);
        var command = BaseCommand with
        {
            StartDate = startDate,
            EndDate = endDate
        };
        var expectedMessage = $"'End Date' must be greater than '{startDate:dd/MM/yyyy}'.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.EndDate)
              .WithErrorMessage(expectedMessage);
    }

    [Fact]
    public async Task Should_HaveValidationError_When_EndDateOverlapsWithExisting()
    {
        // Arrange.
        var command = BaseCommand;
        SetupDoesDateOverlapExistingAsync(true);

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.EndDate)
              .WithErrorMessage("'End Date' overlaps with an existing season.");
    }

    private void SetupDoesDateOverlapExistingAsync(bool returnValue)
    {
        _repositoryMock.Setup(r => r.DoesDateOverlapExistingAsync(It.IsAny<DateOnly>(),
                                                                  It.IsAny<EntityId?>(),
                                                                  It.IsAny<CancellationToken>()))
                       .ReturnsAsync(returnValue);
    }
}
