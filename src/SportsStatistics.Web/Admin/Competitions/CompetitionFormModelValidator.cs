using FluentValidation;

namespace SportsStatistics.Web.Admin.Competitions;

internal sealed class CompetitionFormModelValidator : AbstractValidator<CompetitionFormModel>
{
    public CompetitionFormModelValidator()
    {
        RuleFor(c => c.Season)
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(c => c.CompetitionType)
            .NotEmpty();
    }
}
