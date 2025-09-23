using FluentValidation;

namespace SportsStatistics.Application.Competitions.Delete;

internal sealed class DeleteCompetitionCommandValidator : AbstractValidator<DeleteCompetitionCommand>
{
    public DeleteCompetitionCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(guid => guid.Version == 7)
            .WithMessage("'Id' is not in the correct format.");
    }
}
