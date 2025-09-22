using FluentValidation;

namespace SportsStatistics.Application.Seasons.Create;

internal sealed class CreateSeasonCommandValidator : AbstractValidator<CreateSeasonCommand>
{
    public CreateSeasonCommandValidator(ISeasonRepository repository)
    {
        RuleFor(c => c.StartDate)
            .NotEmpty()
            .LessThan(c => c.EndDate)
            .MustAsync(async (start, cancellation) =>
            {
                var hasOverlap = await repository.DoesDateOverlapExistingAsync(start, null, cancellation);
                return !hasOverlap;
            })
            .WithMessage("'Start Date' overlaps with an existing season.");

        RuleFor(c => c.EndDate)
            .NotEmpty()
            .GreaterThan(c => c.StartDate)
            .MustAsync(async (end, cancellation) =>
            {
                var hasOverlap = await repository.DoesDateOverlapExistingAsync(end, null, cancellation);
                return !hasOverlap;
            })
            .WithMessage("'End Date' overlaps with an existing season.");
    }
}

