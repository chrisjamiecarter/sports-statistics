using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Seasons.Create;
using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Tests.Seasons.Create;

public class CreateSeasonCommandValidatorTests
{
    private static readonly List<Season> BaseSeasons =
    [
        Season.Create(new DateOnly(2023, 8, 1), new DateOnly(2024, 7, 31)),
        Season.Create(new DateOnly(2024, 8, 1), new DateOnly(2025, 7, 31)),
    ];

    private static readonly CreateSeasonCommand BaseCommand = new(new DateOnly(2025, 8, 1),
                                                                  new DateOnly(2026, 7, 31));

    private readonly Mock<DbSet<Season>> _seasonDbSetMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly CreateSeasonCommandValidator _validator;

    public CreateSeasonCommandValidatorTests()
    {
        _seasonDbSetMock = BaseSeasons.BuildMockDbSet();

        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(_seasonDbSetMock.Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
              .ReturnsAsync(1);

        _validator = new CreateSeasonCommandValidator(_dbContextMock.Object);
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
        var command = BaseCommand with
        {
            StartDate = BaseCommand.EndDate.AddDays(1)
        };
        var expectedMessage = $"'Start Date' must be less than '{command.EndDate:dd/MM/yyyy}'.";

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
        var command = BaseCommand with
        {
            StartDate = BaseSeasons[0].StartDate,
        };

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
        var command = BaseCommand with
        {
            EndDate = BaseCommand.StartDate.AddDays(-1)
        };
        var expectedMessage = $"'End Date' must be greater than '{command.StartDate:dd/MM/yyyy}'.";

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
        var command = BaseCommand with
        {
            EndDate = BaseSeasons[0].EndDate,
        };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.EndDate)
              .WithErrorMessage("'End Date' overlaps with an existing season.");
    }
}
