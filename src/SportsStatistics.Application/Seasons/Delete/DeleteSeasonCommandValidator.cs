using FluentValidation;

namespace SportsStatistics.Application.Seasons.Delete;

internal sealed class DeleteSeasonCommandValidator : AbstractValidator<DeleteSeasonCommand>
{
    public DeleteSeasonCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(guid => guid.Version == 7);
    }
}
