using FluentValidation;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Create;

internal sealed class CreateFixtureCommandValidator : AbstractValidator<CreateFixtureCommand>
{
    public CreateFixtureCommandValidator()
    {
        RuleFor(c => c.CompetitionId)
            .NotEmpty().WithError(FixtureErrors.CompetitionIdIsRequired);

        RuleFor(c => c.Opponent)
            .NotEmpty().WithError(FixtureErrors.OpponentIsRequired)
            .MaximumLength(Opponent.MaxLength).WithError(FixtureErrors.OpponentExceedsMaxLength);

        RuleFor(c => c.KickoffTimeUtc)
            .NotEmpty().WithError(FixtureErrors.KickoffDateAndTimeIsRequired);

        RuleFor(c => c.LocationId)
            .Must(Location.ContainsValue).WithError(FixtureErrors.LocationNotFound);
    }
}
