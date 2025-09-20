using FluentValidation;

namespace SportsStatistics.Application.Seasons.Create;

internal sealed class CreateSeasonCommandValidator : AbstractValidator<CreateSeasonCommand>
{
    public CreateSeasonCommandValidator()
    {
        RuleFor(c => c.StartDate)
            .NotEmpty()
            .LessThan(c => c.EndDate);

        RuleFor(c => c.EndDate)
            .NotEmpty()
            .GreaterThan(c => c.StartDate);
    }
}

