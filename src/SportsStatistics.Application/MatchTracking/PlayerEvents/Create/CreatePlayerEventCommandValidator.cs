using FluentValidation;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.PlayerEvents.Create;

internal sealed class CreatePlayerEventCommandValidator : AbstractValidator<CreatePlayerEventCommand>
{
    public CreatePlayerEventCommandValidator()
    {
        RuleFor(c => c.FixtureId)
            .NotEmpty().WithError(PlayerEventErrors.FixtureIdIsRequired);

        RuleFor(c => c.PlayerId)
            .NotEmpty().WithError(PlayerEventErrors.PlayerIdIsRequired);

        RuleFor(c => c.PlayerEventTypeId)
            .Must(PlayerEventType.ContainsValue).WithError(MatchEventBaseErrors.PlayerEventType.Unknown);

        RuleFor(c => c.Minute)
            .GreaterThanOrEqualTo(Domain.MatchTracking.Minute.MinValue).WithError(MatchEventBaseErrors.Minute.BelowMinValue);

        RuleFor(c => c.OccurredAtUtc)
            .NotEmpty().WithError(PlayerEventErrors.OccurredAtDateAndTimeIsRequired);
    }
}
