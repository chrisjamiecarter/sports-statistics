using FluentValidation;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Update;

internal sealed class UpdateFixtureCommandValidator : AbstractValidator<UpdateFixtureCommand>
{
    public UpdateFixtureCommandValidator()
    {
        RuleFor(c => c.FixtureId)
            .NotEmpty().WithError(FixtureErrors.FixtureIdIsRequired);

        RuleFor(c => c.Opponent)
            .NotEmpty().WithError(FixtureErrors.OpponentIsRequired)
            .MaximumLength(Opponent.MaxLength).WithError(FixtureErrors.OpponentExceedsMaxLength);

        RuleFor(c => c.KickoffTimeUtc)
            .NotEmpty().WithError(FixtureErrors.KickoffDateAndTimeIsRequired);

        RuleFor(c => c.LocationId)
            .NotEmpty().WithError(FixtureErrors.LocationIdIsRequired)
            .Must(locationId => Location.Resolve(locationId).IsSuccess).WithError(FixtureErrors.LocationNotFound);
    }
}
