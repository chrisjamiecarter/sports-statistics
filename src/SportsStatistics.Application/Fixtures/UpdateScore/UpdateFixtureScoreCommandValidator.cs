using FluentValidation;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.UpdateScore;

internal sealed class UpdateFixtureScoreCommandValidator : AbstractValidator<UpdateFixtureScoreCommand>
{
    public UpdateFixtureScoreCommandValidator()
    {
        RuleFor(command => command.FixtureId)
            .NotEmpty().WithError(FixtureErrors.FixtureIdIsRequired);

        RuleFor(command => command.FixtureScore)
            .NotNull().WithError(FixtureErrors.FixtureScoreIsRequired);
    }
}
