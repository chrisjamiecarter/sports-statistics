using FluentValidation;

namespace SportsStatistics.Web.MatchTracker;

internal sealed class TeamGoalFormModelValidator : AbstractValidator<TeamGoalFormModel>
{
    public TeamGoalFormModelValidator()
    {
        RuleFor(c => c.IsOwnGoal)
            .NotNull();

        RuleFor(c => c.GoalScorer)
            .NotEmpty().When(model => !model.IsOwnGoal);

        RuleFor(c => c.GoalScorer)
            .NotEqual(model => model.Assister).When(model => !model.IsOwnGoal);

        RuleFor(c => c.Assister)
            .NotEqual(model => model.GoalScorer).When(model => !model.IsOwnGoal);
    }
}
