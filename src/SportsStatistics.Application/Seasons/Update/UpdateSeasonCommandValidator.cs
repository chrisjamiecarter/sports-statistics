using FluentValidation;

namespace SportsStatistics.Application.Seasons.Update;

internal sealed class UpdateSeasonCommandValidator : AbstractValidator<UpdateSeasonCommand>
{
    public UpdateSeasonCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(guid => guid.Version == 7);

        // TODO: Can't be overlapping seasons?
        RuleFor(c => c.StartDate)
            .NotEmpty()
            .LessThan(c => c.EndDate);

        RuleFor(c => c.EndDate)
            .NotEmpty()
            .GreaterThan(c => c.StartDate);
    }
}
