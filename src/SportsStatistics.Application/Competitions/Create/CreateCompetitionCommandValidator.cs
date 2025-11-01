using FluentValidation;
using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Application.Competitions.Create;

internal sealed class CreateCompetitionCommandValidator : AbstractValidator<CreateCompetitionCommand>
{
    public CreateCompetitionCommandValidator()
    {
        RuleFor(c => c.SeasonId)
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
