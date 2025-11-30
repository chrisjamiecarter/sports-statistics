using FluentValidation;
using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Web.Admin.Competitions;

internal sealed class CompetitionFormModelValidator : AbstractValidator<CompetitionFormModel>
{
    public CompetitionFormModelValidator()
    {
        RuleFor(c => c.Season)
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(Name.MaxLength);

        RuleFor(c => c.Format)
            .NotEmpty();
    }
}
