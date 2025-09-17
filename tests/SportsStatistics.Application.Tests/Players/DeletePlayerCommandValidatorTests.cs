using FluentValidation.TestHelper;
using SportsStatistics.Application.Players.Delete;

namespace SportsStatistics.Application.Tests.Players;

public sealed class DeletePlayerCommandValidatorTests
{
    private static readonly DeletePlayerCommand BaseCommand = new(new("01995348-37ea-7cdc-9e06-89d6ef2933db"));

    private readonly DeletePlayerCommandValidator _validator;

    public DeletePlayerCommandValidatorTests()
    {
        _validator = new DeletePlayerCommandValidator();
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
}
