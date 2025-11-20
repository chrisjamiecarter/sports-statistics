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
            .MaximumLength(Name.MaxLength);

        RuleFor(c => c.FormatName)
            .NotEmpty()
            .Must(formatName =>
            {
                return Format.All.Any(format => string.Equals(format.Name, formatName, StringComparison.OrdinalIgnoreCase));
            })
            .WithMessage($"'Format Name' is invalid. Valid options: {string.Join(", ", Format.All)}.");
    }
}
