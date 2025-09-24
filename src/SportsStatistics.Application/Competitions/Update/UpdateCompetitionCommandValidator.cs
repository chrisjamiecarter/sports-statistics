using FluentValidation;
using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Application.Competitions.Update;

internal sealed class UpdateCompetitionCommandValidator : AbstractValidator<UpdateCompetitionCommand>
{
    public UpdateCompetitionCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(guid => guid.Version == 7)
            .WithMessage("'Id' is not in the correct format.");

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(c => c.CompetitionType)
            .NotEmpty()
            .Must(type =>
            {
                return CompetitionType.All.Any(t => string.Equals(t.Name, type, StringComparison.OrdinalIgnoreCase));
            })
            .WithMessage($"Invalid competition type. Valid competition types: {string.Join(", ", CompetitionType.All)}.");
    }
}
