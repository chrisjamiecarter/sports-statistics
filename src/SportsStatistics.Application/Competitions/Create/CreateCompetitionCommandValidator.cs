using FluentValidation;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Create;

internal sealed class CreateCompetitionCommandValidator : AbstractValidator<CreateCompetitionCommand>
{
    public CreateCompetitionCommandValidator()
    {
        RuleFor(c => c.SeasonId)
            .NotEmpty().WithError(CompetitionErrors.SeasonIdIsRequired);

        RuleFor(c => c.Name)
            .NotEmpty().WithError(CompetitionErrors.NameIsRequired)
            .MaximumLength(Name.MaxLength).WithError(CompetitionErrors.NameExceedsMaxLength);

        RuleFor(c => c.FormatId)
            .Must(Format.ContainsValue).WithError(CompetitionErrors.FormatNotFound);
    }
}
