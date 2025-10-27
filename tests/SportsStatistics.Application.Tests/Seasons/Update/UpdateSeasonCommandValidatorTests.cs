using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Seasons.Update;
using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Tests.Seasons.Update;

public class UpdateSeasonCommandValidatorTests
{
    private static readonly List<Season> BaseSeasons =
    [
        Season.Create(new DateOnly(2023, 8, 1), new DateOnly(2024, 7, 31)),
        Season.Create(new DateOnly(2024, 8, 1), new DateOnly(2025, 7, 31)),
        Season.Create(new DateOnly(2025, 8, 1), new DateOnly(2026, 7, 31)),
    ];

    private static readonly UpdateSeasonCommand BaseCommand = new(BaseSeasons[0].Id,
                                                                  BaseSeasons[0].StartDate.AddDays(1),
                                                                  BaseSeasons[0].EndDate.AddDays(-1));

    private readonly Mock<DbSet<Season>> _seasonDbSetMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly UpdateSeasonCommandValidator _validator;

    public UpdateSeasonCommandValidatorTests()
    {
        _seasonDbSetMock = BaseSeasons.BuildMockDbSet();

        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(_seasonDbSetMock.Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _validator = new UpdateSeasonCommandValidator(_dbContextMock.Object);
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
        var command = BaseCommand with { SeasonId = default };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.SeasonId)
              .WithErrorMessage("'Season Id' must not be empty.");
    }

    [Fact]
    public async Task Should_HaveValidationError_When_IdIsNotVersion7()
    {
        // Arrange.
        var command = BaseCommand with { SeasonId = Guid.NewGuid() };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.SeasonId)
              .WithErrorMessage("'Season Id' is not in the correct format.");
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
            StartDate = BaseSeasons[1].StartDate,
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
            EndDate = BaseSeasons[1].EndDate,
        };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.EndDate)
              .WithErrorMessage("'End Date' overlaps with an existing season.");
    }
}
