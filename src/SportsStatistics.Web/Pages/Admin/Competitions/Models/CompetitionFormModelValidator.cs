using FluentValidation;
using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Web.Pages.Admin.Competitions.Models;

internal sealed class CompetitionFormModelValidator : AbstractValidator<CompetitionFormModel>
{
    public CompetitionFormModelValidator()
    {
        RuleFor(c => c.Season)
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(Name.MaxLength);

        RuleFor(c => c.Format)
            .NotEmpty();
    }
}
