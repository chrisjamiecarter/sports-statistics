using FluentValidation;

namespace SportsStatistics.Web.Pages.MatchTracker.Models;

internal sealed class SubstitutionFormModelValidator : AbstractValidator<SubstitutionFormModel>
{
    // TODO: Errors.
    public SubstitutionFormModelValidator()
    {
        RuleFor(model => model.PlayerOn)
            .NotEmpty();
    }
}
