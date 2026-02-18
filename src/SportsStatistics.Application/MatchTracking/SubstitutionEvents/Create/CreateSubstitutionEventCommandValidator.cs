using FluentValidation;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.SubstitutionEvents.Create;

internal sealed class CreateSubstitutionEventCommandValidator : AbstractValidator<CreateSubstitutionEventCommand>
{
    public CreateSubstitutionEventCommandValidator()
    {
        RuleFor(c => c.FixtureId)
            .NotEmpty().WithError(SubstitutionEventErrors.FixtureIdIsRequired);

        RuleFor(c => c.PlayerOffId)
            .NotEmpty().WithError(SubstitutionEventErrors.PlayerOffIdIsRequired);

        RuleFor(c => c.PlayerOnId)
            .NotEmpty().WithError(SubstitutionEventErrors.PlayerOnIdIsRequired);

        RuleFor(c => c.Minute)
            .GreaterThanOrEqualTo(Domain.MatchTracking.Minute.MinValue).WithError(MatchEventBaseErrors.Minute.BelowMinValue);

        RuleFor(c => c.OccurredAtUtc)
            .NotEmpty().WithError(SubstitutionEventErrors.OccurredAtDateAndTimeIsRequired);
    }
}
