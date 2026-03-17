using FluentValidation;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetRecentForm;

internal sealed class GetRecentFormQueryValidator
    : AbstractValidator<GetRecentFormQuery>
{
    public GetRecentFormQueryValidator()
    {
        RuleFor(query => query.SeasonId)
            .NotEmpty().WithError(GetRecentFormQueryErrors.SeasonIdRequired);

        RuleFor(query => query.Count)
            .GreaterThan(0).WithError(GetRecentFormQueryErrors.SeasonIdRequired);
    }
}
