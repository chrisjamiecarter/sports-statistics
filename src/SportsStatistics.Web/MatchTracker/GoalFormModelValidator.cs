using FluentValidation;

namespace SportsStatistics.Web.MatchTracker;

internal sealed class GoalFormModelValidator : AbstractValidator<GoalFormModel>
{
    // TODO: Errors.
    public GoalFormModelValidator()
    {
        RuleFor(model => model.GoalType)
            .IsInEnum();

        RuleFor(model => model.IsOwnGoal)
            .NotNull();

        RuleFor(model => model.GoalScorer)
            .NotEmpty()
            .When(model =>
                (model.GoalType == GoalType.TeamGoal && !model.IsOwnGoal) ||
                (model.GoalType == GoalType.OppositionGoal && model.IsOwnGoal));

        RuleFor(model => model.Assister)
            .NotEqual(model => model.GoalScorer)
            .When(model => model.GoalScorer is not null && model.Assister is not null);
    }
}
