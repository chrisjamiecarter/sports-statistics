using FluentValidation;

namespace SportsStatistics.Web.MatchTracker;

internal sealed class OppositionGoalFormModelValidator : AbstractValidator<OppositionGoalFormModel>
{
    public OppositionGoalFormModelValidator()
    {
        RuleFor(c => c.IsOwnGoal)
            .NotNull();

        RuleFor(c => c.GoalScorer)
            .NotEmpty().When(model => model.IsOwnGoal);
    }
}
