using FluentValidation.TestHelper;
using SportsStatistics.Application.Competitions.Delete;

namespace SportsStatistics.Application.Tests.Competitions.Delete;

public sealed class DeleteCompetitionCommandValidatorTests
{
    private static readonly DeleteCompetitionCommand BaseCommand = new(new("01995348-37ea-7cdc-9e06-89d6ef2933db"));

    private readonly DeleteCompetitionCommandValidator _validator;

    public DeleteCompetitionCommandValidatorTests()
    {
        _validator = new DeleteCompetitionCommandValidator();
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

    [Fact]
    public async Task Should_HaveValidationError_When_IdIsNotVersion7()
    {
        // Arrange.
        var command = BaseCommand with { Id = Guid.NewGuid() };

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Id)
              .WithErrorMessage("'Id' is not in the correct format.");
    }
}
