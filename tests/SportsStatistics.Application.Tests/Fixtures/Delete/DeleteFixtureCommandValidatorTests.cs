using FluentValidation.TestHelper;
using SportsStatistics.Application.Fixtures.Delete;

namespace SportsStatistics.Application.Tests.Fixtures.Delete;

public sealed class DeleteFixtureCommandValidatorTests
{
    private static readonly DeleteFixtureCommand BaseCommand = new(Guid.CreateVersion7());

    private readonly DeleteFixtureCommandValidator _validator;

    public DeleteFixtureCommandValidatorTests()
    {
        _validator = new DeleteFixtureCommandValidator();
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
        var expectedErrorMessage = "'Id' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Id)
              .WithErrorMessage(expectedErrorMessage);
    }

    [Fact]
    public async Task Should_HaveValidationError_When_IdIsNotVersion7()
    {
        // Arrange.
        var command = BaseCommand with { Id = Guid.NewGuid() };
        var expectedErrorMessage = "'Id' is not in the correct format.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Id)
              .WithErrorMessage(expectedErrorMessage);
    }
}
