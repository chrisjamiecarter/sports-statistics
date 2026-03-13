using FluentValidation;

namespace SportsStatistics.Web.MatchTracker;

internal sealed class SubstitutionFormModelValidator : AbstractValidator<SubstitutionFormModel>
{
    // TODO: Errors.
    public SubstitutionFormModelValidator()
    {
        RuleFor(model => model.PlayerOn)
            .NotEmpty();
    }
}
