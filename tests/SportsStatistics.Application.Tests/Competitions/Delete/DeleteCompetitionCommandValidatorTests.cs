﻿using FluentValidation.TestHelper;
using SportsStatistics.Application.Competitions.Delete;

namespace SportsStatistics.Application.Tests.Competitions.Delete;

public sealed class DeleteCompetitionCommandValidatorTests
{
    private static readonly DeleteCompetitionCommand BaseCommand = new(Guid.CreateVersion7());

    private readonly DeleteCompetitionCommandValidator _validator;

    public DeleteCompetitionCommandValidatorTests()
    {
        _validator = new DeleteCompetitionCommandValidator();
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
    public async Task ValidateAsync_ShouldHaveValidationError_WhenIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { CompetitionId = default };
        var expected = "'Competition Id' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.CompetitionId)
              .WithErrorMessage(expected);
    }
}
