using FluentValidation;

namespace SportsStatistics.Web.Admin.Seasons;

internal sealed class SeasonFormModelValidator : AbstractValidator<SeasonFormModel>
{
    public SeasonFormModelValidator()
    {
        RuleFor(c => c.StartDate)
            .NotEmpty()
            .Must((model, start) => start < model.EndDate)
            .WithMessage("'Start Date' must be before 'End Date'.");

        RuleFor(c => c.EndDate)
            .NotEmpty()
            .Must((model, end) => end > model.StartDate)
            .WithMessage("'End Date' must be after 'Start Date'.");
    }
}
