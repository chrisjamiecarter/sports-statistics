using FluentValidation;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Update;

internal sealed class UpdateCompetitionCommandValidator : AbstractValidator<UpdateCompetitionCommand>
{
    public UpdateCompetitionCommandValidator()
    {
        RuleFor(c => c.CompetitionId)
            .NotEmpty().WithError(CompetitionErrors.CompetitionIdIsRequired);

        RuleFor(c => c.Name)
            .NotEmpty().WithError(CompetitionErrors.NameIsRequired)
            .MaximumLength(Name.MaxLength).WithError(CompetitionErrors.NameExceedsMaxLength);

        RuleFor(c => c.FormatId)
            .Must(Format.ContainsValue).WithError(CompetitionErrors.FormatNotFound);
    }
}
