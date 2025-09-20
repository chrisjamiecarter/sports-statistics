using FluentValidation;

namespace SportsStatistics.Application.Seasons.Delete;

internal sealed class DeleteSeasonCommandValidator : AbstractValidator<DeleteSeasonCommand>
{
    public DeleteSeasonCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(id => id.Version == 7);
    }
}
