using FluentValidation;

namespace SportsStatistics.Web.Admin.Fixtures;

internal sealed class FixtureFormModelValidator : AbstractValidator<FixtureFormModel>
{
    public FixtureFormModelValidator()
    {
        RuleFor(f => f.Competition)
            .NotEmpty();

        RuleFor(f => f.KickoffTimeUtc)
            .NotEmpty();

        RuleFor(f => f.Location)
            .NotEmpty();

        RuleFor(c => c.Opponent)
            .NotEmpty()
            .MaximumLength(100);
    }
}