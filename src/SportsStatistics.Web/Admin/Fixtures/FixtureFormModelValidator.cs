using FluentValidation;

namespace SportsStatistics.Web.Admin.Fixtures;

internal sealed class FixtureFormModelValidator : AbstractValidator<FixtureFormModel>
{
    public FixtureFormModelValidator()
    {
        RuleFor(f => f.CompetitionId)
            .NotEmpty();

        RuleFor(c => c.Opponent)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(f => f.KickoffTimeUtc)
            .NotEmpty();

        RuleFor(f => f.LocationName)
            .NotEmpty()
            .MaximumLength(7);
    }
}