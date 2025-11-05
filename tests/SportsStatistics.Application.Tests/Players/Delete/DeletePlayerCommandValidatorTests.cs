using FluentValidation.TestHelper;
using SportsStatistics.Application.Players.Delete;

namespace SportsStatistics.Application.Tests.Players.Delete;

public sealed class DeletePlayerCommandValidatorTests
{
    private static readonly DeletePlayerCommand BaseCommand = new(Guid.CreateVersion7());

    private readonly DeletePlayerCommandValidator _validator;

    public DeletePlayerCommandValidatorTests()
    {
        _validator = new DeletePlayerCommandValidator();
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
}
