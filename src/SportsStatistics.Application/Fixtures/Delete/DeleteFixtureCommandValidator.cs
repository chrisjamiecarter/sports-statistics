using FluentValidation;

namespace SportsStatistics.Application.Fixtures.Delete;

internal sealed class DeleteFixtureCommandValidator : AbstractValidator<DeleteFixtureCommand>
{
    public DeleteFixtureCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(guid => guid.Version == 7)
            .WithMessage("'Id' is not in the correct format.");
    }
}
