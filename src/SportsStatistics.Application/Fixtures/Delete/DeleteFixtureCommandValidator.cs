using FluentValidation;

namespace SportsStatistics.Application.Fixtures.Delete;

internal sealed class DeleteFixtureCommandValidator : AbstractValidator<DeleteFixtureCommand>
{
    public DeleteFixtureCommandValidator()
    {
        RuleFor(c => c.FixtureId)
            .NotEmpty();
    }
}
