using FluentValidation;

namespace SportsStatistics.Application.Competitions.Delete;

internal sealed class DeleteCompetitionCommandValidator : AbstractValidator<DeleteCompetitionCommand>
{
    public DeleteCompetitionCommandValidator()
    {
        RuleFor(c => c.CompetitionId)
            .NotEmpty();
    }
}
