using FluentValidation;

namespace SportsStatistics.Application.Fixtures.GetBySeasonId;

internal sealed class GetFixturesBySeasonIdQueryValidator : AbstractValidator<GetFixturesBySeasonIdQuery>
{
    public GetFixturesBySeasonIdQueryValidator()
    {
        RuleFor(c => c.SeasonId)
            .NotEmpty();
    }
}
