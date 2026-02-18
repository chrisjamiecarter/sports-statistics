using FluentValidation;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.MatchEvents.Create;

internal sealed class CreateMatchEventCommandValidator : AbstractValidator<CreateMatchEventCommand>
{
    public CreateMatchEventCommandValidator()
    {
        RuleFor(c => c.FixtureId)
            .NotEmpty().WithError(MatchEventErrors.FixtureIdIsRequired);

        RuleFor(c => c.MatchEventTypeId)
            .Must(MatchEventType.ContainsValue).WithError(MatchEventBaseErrors.MatchEventType.Unknown);

        RuleFor(c => c.Minute)
            .GreaterThanOrEqualTo(Minute.MinValue).WithError(MatchEventBaseErrors.Minute.BelowMinValue);

        RuleFor(c => c.OccurredAtUtc)
            .NotEmpty().WithError(MatchEventErrors.OccurredAtDateAndTimeIsRequired);
    }
}
