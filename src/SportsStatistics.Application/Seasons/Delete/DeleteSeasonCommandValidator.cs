using FluentValidation;

namespace SportsStatistics.Application.Seasons.Delete;

internal sealed class DeleteSeasonCommandValidator : AbstractValidator<DeleteSeasonCommand>
{
    public DeleteSeasonCommandValidator()
    {
        RuleFor(c => c.SeasonId)
            .NotEmpty()
            .Must(guid => guid.Version == 7)
            .WithMessage("'Season Id' is not in the correct format.");
    }
}
