using FluentValidation;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Web.Pages.Admin.Competitions.Models;

internal sealed class CompetitionFormModelValidator : AbstractValidator<CompetitionFormModel>
{
    public CompetitionFormModelValidator()
    {
        RuleFor(c => c.Season)
            .NotEmpty().WithError(CompetitionErrors.SeasonIdIsRequired);

        RuleFor(c => c.Name)
            .NotEmpty().WithError(CompetitionErrors.NameIsRequired)
            .MaximumLength(Name.MaxLength).WithError(CompetitionErrors.NameExceedsMaxLength);

        RuleFor(c => c.Format)
            .NotEmpty().WithError(CompetitionErrors.FormatNotFound);
    }
}
