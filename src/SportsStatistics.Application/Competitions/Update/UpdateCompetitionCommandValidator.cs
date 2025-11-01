using FluentValidation;
using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Application.Competitions.Update;

internal sealed class UpdateCompetitionCommandValidator : AbstractValidator<UpdateCompetitionCommand>
{
    public UpdateCompetitionCommandValidator()
    {
        RuleFor(c => c.CompetitionId)
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(c => c.CompetitionTypeName)
            .NotEmpty()
            .Must(type =>
            {
                return CompetitionType.All.Any(t => string.Equals(t.Name, type, StringComparison.OrdinalIgnoreCase));
            })
            .WithMessage($"'Competition Type Name' is invalid. Valid options: {string.Join(", ", CompetitionType.All)}.");
    }
}
