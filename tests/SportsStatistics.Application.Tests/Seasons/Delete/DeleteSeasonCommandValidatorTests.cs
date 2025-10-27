using FluentValidation.TestHelper;
using SportsStatistics.Application.Seasons.Delete;

namespace SportsStatistics.Application.Tests.Seasons.Delete;

public sealed class DeleteSeasonCommandValidatorTests
{
    private static readonly DeleteSeasonCommand BaseCommand = new(Guid.CreateVersion7());

    private readonly DeleteSeasonCommandValidator _validator;

    public DeleteSeasonCommandValidatorTests()
    {
        _validator = new DeleteSeasonCommandValidator();
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
}
